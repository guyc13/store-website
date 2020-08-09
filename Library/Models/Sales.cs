using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewStore.Models
{
    public class Sales
    {
        public int SalesID { get; set; }
        
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public virtual Item Items { get; set; }
        public int ItemID { get; set; }

        public bool Active { get; set; }
       

    }
}