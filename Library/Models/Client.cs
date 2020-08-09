using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewStore.Models
{
    public class Client
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Client Name")]
        public int ClientID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string ClientFirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string ClientLastName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Age { get; set; }



        public virtual ICollection<Sales> Sales { get; set; }

    }
}