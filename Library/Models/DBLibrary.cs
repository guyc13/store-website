using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewLibrary.Models
{
    public class DBLibrary : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Loans> Loans { get; set; }



    }
}