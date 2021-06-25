using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryAssistant.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(60)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Range(1, 22500)]
        public int Pages { get; set; }

        public bool isTaken {get;set;}

        [StringLength(3000)]
        public string Resume {get;set;}

        [DefaultValue(false)] 
        public bool Deleted {get; set;}

        public int? CustomerId {get;set;}

        public int? AuthorId {get;set;}

        public virtual Author Author {get;set;}
        public virtual Customer Customer {get;set;}
        public ICollection<BookPossessionHistory> BookPossessionHistories {get;set;}

        // public ICollection<BookHasAuthor> BookHasAuthors {get; set;}
    }
}