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
    public class ClientsController : Controller
    {
        private DBStore db = new DBStore();

        // GET: Client
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Firstname = "";
            ViewBag.Lastname = "";
            ViewBag.age = "";

            return View(db.Clients.ToList());
        }
        [HttpPost]
        public ActionResult Index(string firstname, string age, string lastname)
        {
            ViewBag.firstname = firstname;
            ViewBag.lastname = lastname;
            ViewBag.age = age;

            var client = db.Clients.ToList().Where(p => p.ClientFirstName.StartsWith(firstname) && p.Age.StartsWith(age) && p.ClientLastName.StartsWith(lastname));
            return View(client.ToList());

        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,ClientFirstName,ClientLastName,Password,Age")] Client client)
        {
            var validateName = db.Clients.FirstOrDefault(x => x.ClientFirstName == client.ClientFirstName && x.ClientLastName == client.ClientLastName);
            if (validateName == null)
            {
                if (ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                return View(client);
            }


            else return HttpNotFound();
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,ClientFirstName,ClientLastName,Password,Age")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            foreach (Sales l in db.Sales)
            {
                if (l.Active)
                {
                    db.Items.Find(l.ItemID).Stock++;
                }
                if (l.ClientID == client.ClientID)
                    db.Sales.Remove(l);

            }
            db.Clients.Remove(client);
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
