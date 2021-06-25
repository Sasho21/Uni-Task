using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryAssistant.Models;

    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;port=5432;Database=library;Username=postgres;Password=pass");

        public DbSet<LibraryAssistant.Models.Book> Book { get; set; }

        public DbSet<LibraryAssistant.Models.Author> Author { get; set; }

        public DbSet<LibraryAssistant.Models.Customer> Customer { get; set; }

        public DbSet<LibraryAssistant.Models.BookPossessionHistory> BookPossessionHistory { get; set; }
    }
