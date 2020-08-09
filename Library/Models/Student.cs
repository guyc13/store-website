using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLibrary.Models
{
    public class Student
    {
        [Required]
        public int StudentID { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Age { get; set; }

        public virtual ICollection<Loans> Loans { get; set; }

    }
}