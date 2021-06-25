using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryAssistant.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAssistant.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchTitle, string searchResume)
        {
            var books = from m in _context.Book
                        select m;

            if (!String.IsNullOrEmpty(searchTitle))
            {
                books = books.Where(s => s.Title.Contains(searchTitle));
            }

            if (!String.IsNullOrEmpty(searchResume))
            {
                books = books.Where(s => s.Resume.Contains(searchResume));
            }

            return View(await books.Where(t => t.Deleted == false).Include(t => t.Author).ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var authorsQuery = from m in _context.Author
                                    where m.Deleted == false
                                    orderby m.FirstName
                                    select new KeyValuePair<int, string> (m.Id, (m.FirstName + " " + m.LastName));
                                    
            ViewData["Authors"] = new SelectList(authorsQuery.AsEnumerable().ToList(), "Key", "Value");
            
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PublishDate,Pages,FeePerDay,isTaken,Resume")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsQuery = from m in _context.Author
                                    orderby m.FirstName
                                    select new KeyValuePair<int, string> (m.Id, (m.FirstName + " " + m.LastName));

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(authorsQuery.AsEnumerable().ToList(), "Key", "Value");

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PublishDate,Pages,FeePerDay,isTaken,Resume,AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string deleteButton)
        {
            var book = await _context.Book.FindAsync(id);
            if (deleteButton == "Delete") {
                book.Deleted = true;
                _context.Update(book);
            } else {
                _context.Book.Remove(book);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
