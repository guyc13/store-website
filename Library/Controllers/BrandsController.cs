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

    public class BrandsController : Controller
    {
        private DBStore db = new DBStore();

        // GET: Brands
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            ViewBag.name = name;

            var brand = db.Brands.ToList().Where(p => p.BrandName.StartsWith(name));
            return View(brand.ToList());
        }





        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID,BrandName")] Brand brand)
        {
            var validateName = db.Brands.FirstOrDefault(x => x.BrandName == brand.BrandName);
            if (validateName == null)
            {
                if (ModelState.IsValid)
                {
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                return View(brand);
            }


            else return HttpNotFound();

        }


        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,BrandName")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            foreach (Item l in db.Items)
            {
                foreach (Sales k in db.Sales)
                {
                    if (k.ItemID == l.ItemID)
                    {

                        db.Sales.Remove(k);
                    }
                }
                if (l.BrandID == brand.BrandID)
                    db.Items.Remove(l);
            }



            db.Brands.Remove(brand);
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
