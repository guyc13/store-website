using NewStore.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TheBestStoreEver.Controllers;

namespace Jstore.Controllers
{
    public class ItemsController : HomeController
    {
        private DBStore db = new DBStore();
        public HomeController homec;

        // GET: Items
        [HttpGet]
        public ActionResult ItemIndex()
        {
            ViewBag.name = "";
            ViewBag.type = "";
            ViewBag.stock = "";

            var items = db.Items.Include(b => b.Brand);
            return View(items);
        }
        [HttpPost]
        public ActionResult ItemIndex(string name, string type, int? stock)
        {
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.stock = stock;


            var items = db.Items.ToList().Where(p => (p.ItemName.StartsWith(name) && p.ItemType.StartsWith(type)));
            if (stock != null)
            {
                var b = items.ToList().Where(p => p.Stock.Equals(stock));
                return View(b.ToList());

            }

            return View(items.ToList());
        }



        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }


        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemName,itemType,Stock,BrandID")] Item item)
        {
            var validateName = db.Items.FirstOrDefault(x => x.ItemName == item.ItemName);
            if (validateName == null)
            {
                if (ModelState.IsValid)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("ItemIndex");
                }


                return View(item);
            }


            else return HttpNotFound();
        }



        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", item.BrandID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemName,ItemType,Stock,BrandID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ItemIndex");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", item.BrandID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
                //return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            foreach (Sales l in db.Sales)
            {
                if (l.ItemID == item.ItemID)

                    db.Sales.Remove(l);
            }
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("ItemIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult OurItems(int? id)
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }
            ViewBag.Message = "The items";
            var items = db.Items.Include(b => b.Brand);
            return View(items);


        }
        [HttpPost]
        public ActionResult OurItems(string name, string type, int? stock)
        {
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.stock = stock;
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            var items = db.Items.ToList().Where(p => (p.ItemName.StartsWith(name) && p.ItemType.StartsWith(type)));
            if (stock != null)
            {
                var b = items.ToList().Where(p => p.Stock.Equals(stock));
                return View(b.ToList());

            }

            return View(items.ToList());
        }

        public ActionResult Catalog(int? id)
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            ViewBag.Message = "Catalog";

            return View();


        }



        [HttpGet]
        public ActionResult Group()
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            var group = (from bo in db.Items
                         group bo by bo.ItemType into j
                         select new Group<string, Item> { Key = j.Key, Values = j });

            return View(group.ToList());
        }

        [HttpGet]
        public ActionResult BasicGroup()
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            var group = (from bo in db.Items
                         group bo by bo.ItemType into j
                         select new Group<string, Item> { Key = j.Key, Values = j });

            return View(group.ToList());
        }


        [HttpGet]
        public ActionResult Statistics()
        {

            ICollection<Stat> mylist = new Collection<Stat>();
            var r = (from bo in db.Items
                     group bo by bo.ItemType into j
                     select j);

            foreach (var v in r)
            {
                mylist.Add(new Stat(v.Key, v.Count()));

            }

            ViewBag.data = mylist;

            ICollection<Stat> mylist2 = new Collection<Stat>();

            var q = (from lo in db.Sales
                     join bo in db.Items
                     on lo.ItemID equals bo.ItemID
                     where lo.ItemID == bo.ItemID
                     group bo by bo.ItemName into j
                     select j);

            foreach (var v in q)
            {
                mylist2.Add(new Stat(v.Key, v.Count()));

            }

            ViewBag.data2 = mylist2;

            return View();
        }

        [HttpGet]
        public ActionResult BasicStatistics()
        {
            if (ClientName == "")
            {
                ViewBag.Admin = "true";
            }

            ICollection<Stat> mylist = new Collection<Stat>();
            var r = (from bo in db.Items
                     group bo by bo.ItemType into j
                     select j);

            foreach (var v in r)
            {
                mylist.Add(new Stat(v.Key, v.Count()));

            }

            ViewBag.data = mylist;

            ICollection<Stat> mylist2 = new Collection<Stat>();

            var q = (from lo in db.Sales
                     join bo in db.Items
                     on lo.ItemID equals bo.ItemID
                     where lo.ItemID == bo.ItemID
                     group bo by bo.ItemName into j
                     select j);

            foreach (var v in q)
            {
                mylist2.Add(new Stat(v.Key, v.Count()));

            }

            ViewBag.data2 = mylist2;

            return View();
        }

    }



    public class Group<K, T>
    {
        public K Key { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
    public class Stat
    {
        public string Key;
        public int Values;


        public Stat(string key, int values)
        {
            Key = key;
            Values = values;
        }
    }




}
