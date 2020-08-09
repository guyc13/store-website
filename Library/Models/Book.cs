using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLibrary.Models
{
    public class Book
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookGenre { get; set; }
        [Required]
        public int Stock { get; set; }
        public int AuthorID { get; set; }

        public virtual ICollection<Loans> Loans { get; set; }



    }
}