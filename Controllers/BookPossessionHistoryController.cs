using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryAssistant.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace LibraryAssistant.Controllers
{
    [Authorize]
    public class BookPossessionHistoryController : Controller
    {
        private readonly LibraryContext _context;

        public BookPossessionHistoryController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookPossessionHistory
        public async Task<IActionResult> Index(string searchLendFrom, string searchLendTo, string searchReturnFrom, string searchReturnTo, string searchTitle)
        {
            var bookPossessionHistory = from m in _context.BookPossessionHistory
                        select m;

            if (!String.IsNullOrEmpty(searchLendFrom))
            {
                bookPossessionHistory = bookPossessionHistory.Where(s => s.LendDate > DateTime.ParseExact(searchLendFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(searchLendTo))
            {
                bookPossessionHistory = bookPossessionHistory.Where(s => s.LendDate < DateTime.ParseExact(searchLendTo, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(searchReturnFrom))
            {
                bookPossessionHistory = bookPossessionHistory.Where(s => s.ReturnDate > DateTime.ParseExact(searchReturnFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(searchReturnTo))
            {
                bookPossessionHistory = bookPossessionHistory.Where(s => s.ReturnDate < DateTime.ParseExact(searchReturnTo, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(searchTitle))
            {
                bookPossessionHistory = bookPossessionHistory.Where(s => s.Book.Title.Contains(searchTitle));
            }

            return View(await bookPossessionHistory.Include(t => t.Book).Include(t => t.Customer).ToListAsync());
        }

        // GET: BookPossessionHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPossessionHistory = await _context.BookPossessionHistory.Include(t => t.Book).Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookPossessionHistory == null)
            {
                return NotFound();
            }

            return View(bookPossessionHistory);
        }

        // GET: BookPossessionHistory/Create
        public IActionResult Create()
        {
            var customersQuery = from m in _context.Customer
                                    where m.Deleted == false
                                    orderby m.FirstName
                                    select new KeyValuePair<int, string> (m.Id, (m.FirstName + " " + m.LastName));
                                    
            ViewData["Customers"] = new SelectList(customersQuery.AsEnumerable().ToList(), "Key", "Value");

            var booksQuery = from m in _context.Book
                                    where m.isTaken == false && m.Deleted == false
                                    orderby m.Title
                                    select new KeyValuePair<int, string> (m.Id, m.Title);
                                    
            ViewData["Books"] = new SelectList(booksQuery.AsEnumerable().ToList(), "Key", "Value");
            
            return View();
        }

        // POST: BookPossessionHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,BookId,LendDate,ReturnDate,AmountDue")] BookPossessionHistory bookPossessionHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookPossessionHistory);
                var book = await _context.Book.FindAsync(bookPossessionHistory.BookId);
                var customer = await _context.Customer.FindAsync(bookPossessionHistory.CustomerId);
                book.isTaken = true;
                customer.DueTotal += bookPossessionHistory.AmountDue;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookPossessionHistory);
        }

        // GET: BookPossessionHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersQuery = from m in _context.Customer
                                    where m.Deleted == false
                                    orderby m.FirstName
                                    select new KeyValuePair<int, string> (m.Id, (m.FirstName + " " + m.LastName));
                                    
            ViewData["Customers"] = new SelectList(customersQuery.AsEnumerable().ToList(), "Key", "Value");

            var booksQuery = from m in _context.Book
                                    where m.Deleted == false 
                                    orderby m.Title
                                    select new KeyValuePair<int, string> (m.Id, m.Title);
                                    
            ViewData["Books"] = new SelectList(booksQuery.AsEnumerable().ToList(), "Key", "Value");

            var bookPossessionHistory = await _context.BookPossessionHistory.FindAsync(id);
            if (bookPossessionHistory == null)
            {
                return NotFound();
            }

            var smt = (DateTime.Now - bookPossessionHistory.ReturnDate).Days;

            return View(bookPossessionHistory);
        }

        // POST: BookPossessionHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,CustomerId,LendDate,ReturnDate,AmountDue")] BookPossessionHistory bookPossessionHistory, string submitButton)
        {
            if (id != bookPossessionHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await _context.Customer.FindAsync(bookPossessionHistory.CustomerId);
                    var beforeUpdateHistory = await _context.BookPossessionHistory.FindAsync(bookPossessionHistory.Id);
                    var previousTax = beforeUpdateHistory.AmountDue;
                    _context.Entry(beforeUpdateHistory).State = EntityState.Detached;

                    if (submitButton == "Update") {
                        customer.DueTotal += (bookPossessionHistory.AmountDue - previousTax);
                    } else {
                        var book = await _context.Book.FindAsync(bookPossessionHistory.BookId);
                        book.isTaken = false;
                        bookPossessionHistory.Returned = true;
                        customer.DueTotal -= previousTax;
                    }
                    _context.Update(bookPossessionHistory);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookPossessionHistoryExists(bookPossessionHistory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookPossessionHistory);
        }

        // GET: BookPossessionHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookPossessionHistory = await _context.BookPossessionHistory.Include(t => t.Book).Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookPossessionHistory == null)
            {
                return NotFound();
            }

            return View(bookPossessionHistory);
        }

        // POST: BookPossessionHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookPossessionHistory = await _context.BookPossessionHistory.FindAsync(id);
            _context.BookPossessionHistory.Remove(bookPossessionHistory);
            var book = await _context.Book.FindAsync(bookPossessionHistory.BookId);
            var customer = await _context.Customer.FindAsync(bookPossessionHistory.CustomerId);
            book.isTaken = false;
            customer.DueTotal -= bookPossessionHistory.AmountDue;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookPossessionHistoryExists(int id)
        {
            return _context.BookPossessionHistory.Any(e => e.Id == id);
        }
    }
}
