using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewStore.Models
{
    public class Item
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Item Name")]
        public int ItemID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string ItemType { get; set; }
        [Required]
        public int Stock { get; set; }
        public int BrandID { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }



    }
}