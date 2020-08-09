using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLibrary.Models
{
    public class Author
    {
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public virtual ICollection<Book> Books { get; set; }


    }
}