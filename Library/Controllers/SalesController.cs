using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewStore.Models;

namespace Jstore.Controllers
{
    public class SalesController : Controller
    {
        private DBStore db = new DBStore();

        [HttpGet]
        public ActionResult Index()
        {
            var sales = db.Sales.Include(l => l.Items).Include(l => l.Client);
            return View(sales.ToList());
        }
        [HttpPost]
        public ActionResult Index(string item)
        {
            ViewBag.item = item;

            var Itemsid = (from bo in db.Items
                           join lo in db.Sales
                           on bo.ItemID equals lo.ItemID
                           where bo.ItemName.StartsWith(item)
                           select lo);

            return View(Itemsid.ToList());

        }

        // GET: sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: sales/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName");
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientFirstName");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesID,ClientID,ItemID,Active")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                if (sales.Active)
                {
                    var stock = db.Items.ToList().Where(p => p.ItemID.Equals(sales.ItemID) && p.Stock > 0);
                    if (stock.Count() <= 0)
                    {
                        ModelState.AddModelError("", "The item is out of stock. Buy another item.");
                    }
                    else
                    {
                        db.Sales.Add(sales);
                        db.Items.Find(sales.ItemID).Stock--;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.ItemID = new SelectList(db.Items, "itemID", "ItemName", sales.ItemID);
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientFirstName", sales.ClientID);
            return View(sales);
        }

        // GET: sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", sales.ItemID);
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientFirstName", sales.ClientID);
            return View(sales);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesID,ClientID,ItemID,Active")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                if (!sales.Active)
                {
                    db.Items.Find(sales.ItemID).Stock++;
                }
                else
                {
                    db.Items.Find(sales.ItemID).Stock--;
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", sales.ItemID);
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientFirstName", sales.ClientID);
            return View(sales);
        }

        // GET: sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            if (sales.Active)
                db.Items.Find(sales.ItemID).Stock++;

            db.Sales.Remove(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
