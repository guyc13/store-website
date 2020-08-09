using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewLibrary.Models
{
    public class Loans
    {
        public int LoansID { get; set; }
        public Student student { get; set; }
        public int StudentID { get; set; }

        public Book Books { get; set; }
        public int BookID { get; set; }

        public bool Active { get; set; }
        public string peopel { get; set; }

    }
}