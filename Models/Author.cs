using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryAssistant.Models
{
    public class Author
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

        [DefaultValue(false)]        
        public bool Deleted {get; set;}

        [StringLength(3000)]
        public string About {get;set;}

        public ICollection<Book> Books {get; set;}
    }
}