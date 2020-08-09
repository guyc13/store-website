using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewStore.Models
{
    public class Brand
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Brand Name")]
        public int BrandID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } 

        public virtual ICollection<Item> Items { get; set; }


    }
}