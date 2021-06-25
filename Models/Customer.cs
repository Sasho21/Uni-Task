using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryAssistant.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(60)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(60)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Phone]
        [StringLength(10)]
        [Required]
        public string Phone {get;set;}

        [EmailAddress]
        [Required]
        public string Email {get;set;}

        [DefaultValue(0.00)]
        public decimal DueTotal {get;set;}

        [DefaultValue(false)] 
        public bool Deleted {get; set;}

        public ICollection<Book> Books {get;set;}
    }
}