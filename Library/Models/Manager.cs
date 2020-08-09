using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewLibrary.Models
{
    public class Manager
    {
        [Required]
        public int ManagerID { get; set; }
        [Required]
        public string ManagerName { get; set; }
        [Required]
        public string ManagerPassword { get; set; }


    }
}