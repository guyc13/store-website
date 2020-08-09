using NewStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jstore.Controllers;

namespace TheBestStoreEver.Controllers
{
    public class HomeController : Controller
    {
        IDictionary<string, string> ManagerMap = new Dictionary<string, string>();
        IDictionary<string, string> ClientMap = new Dictionary<string, string>();
        public static string ClientName="";
        

        private DBStore db = new DBStore();

        [HttpGet]
        public ActionResult Index()
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
          

            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string pass)
        {
            ViewBag.name = name;
            ViewBag.pass = pass;
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            foreach (Manager m in db.Managers)
            {
                ManagerMap.Add(m.ManagerName, m.ManagerPassword);

            }
            foreach (Client m in db.Clients)
            {
                ClientMap.Add(m.ClientFirstName, m.Password);

            }

            if (ManagerMap.ContainsKey(name))
            {
                if (ManagerMap[name].Equals(pass))
                {
                    
                    return RedirectToAction("ManHome");
                }
                else return RedirectToAction("Index");
            }
            else if(ClientMap.ContainsKey(name))
            {
                if (ClientMap[name].Equals(pass))
                {
                    ClientName = name;
                  
                    return RedirectToAction("ClientHome");
                }
                else return RedirectToAction("Index");

            }

            else return RedirectToAction("Index");
        }



        public ActionResult About()
        {
            

            return View();
        }
        public ActionResult BasicAbout()
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            return View();
        }

        public ActionResult ManHome()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Admin = "Your contact page.";

            return View();
        }
        public ActionResult BasicContact()
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }
            return View();
        }


        public ActionResult ClientHome()
        {
           
            var sales = (from bo in db.Clients
                         join lo in db.Sales
                         on bo.ClientID equals lo.ClientID
                         where bo.ClientFirstName.StartsWith(ClientName)
                         select lo);


            var item = (from bo in db.Items
                        join lo in sales
                        on bo.ItemID equals lo.ItemID
                        where bo.ItemID == lo.ItemID
                        select new { itemname = bo.ItemName, Brand = bo.BrandID, type = bo.ItemType });

            var total = (from bo in db.Brands
                         join lo in item
                         on bo.BrandID equals lo.Brand
                         where bo.BrandID == lo.Brand
                         select new { itemname = lo.itemname, Brand = bo.BrandName, type = lo.type });

            ICollection<Items> list = new Collection<Items>();

            foreach (var v in total)
            {
                list.Add(new Items(total.Count(), v.itemname, v.Brand, v.type));

            }
            ViewBag.data = list;

            ICollection<Stat> gList = new Collection<Stat>();


            var item2 = (from bo in db.Items
                        join lo in sales
                        on bo.ItemID equals lo.ItemID
                        where bo.ItemID == lo.ItemID
                        group bo by bo.ItemType into j
                        select j);

            foreach(var v in item2)
            {
                gList.Add(new Stat(v.Key, v.Count()));

            }
           
            int max = 0;
            foreach(var c in gList)
            {
                if (c.Values > max)
                {
                    max = c.Values;
                    ViewBag.type = c.Key;
                }
            }

            return View();


        }

        public ActionResult Logout()
        {
            ClientName = "";

            ViewBag.Admin = "";

            return View();
        }


        public ActionResult GoogleMaps()
        {
            return View();
        }
        public JsonResult ResGoogleMap()
        {
            double[] data = { 31.969850, 34.770989 };
            var c = data.ToList();
            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }

    }

    public class Items
    {
        int num;
        public string itemName;
        public string BrandName;
        public string itemtype;

        public Items(int num, string itemName, string BrandName, string itemtype)
        {
            this.num = num;
            this.itemName = itemName;
            this.BrandName = BrandName;
            this.itemtype = itemtype;
        }

        public Items()
        {
        }
    }
}