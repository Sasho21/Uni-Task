using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryAssistant.Models
{
    public class BookPossessionHistory
    {
        public int Id {get;set;}

        [Required]
        public int BookId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Lend Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime LendDate { get; set; }

        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Required]
        public decimal AmountDue {get;set;}

        public bool Returned {get; set;}
        public virtual Book Book {get;set;}
        public virtual Customer Customer {get;set;}
    }
}