using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineJokes.Models;
using OnlineJokes.Context;

namespace OnlineJokes.Controllers
{
    public class CategoryController : Controller
    {
        private JokesContext db = new JokesContext();

        // GET: /Category/
        public ActionResult Index()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("LogIn", "Authentication");
            }
            else
            {
                return View(db.JokesCategorys.ToList());
            }
            
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JokesCategory jokescategory = db.JokesCategorys.Find(id);
            if (jokescategory == null)
            {
                return HttpNotFound();
            }
            return View(jokescategory);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("LogIn", "Authentication");
            }
            else
            {
                return View();
            }
           
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CategoryName")] JokesCategory jokescategory)
        {
            if (ModelState.IsValid)
            {
                db.JokesCategorys.Add(jokescategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jokescategory);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JokesCategory jokescategory = db.JokesCategorys.Find(id);
            if (jokescategory == null)
            {
                return HttpNotFound();
            }
            return View(jokescategory);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CategoryName")] JokesCategory jokescategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jokescategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jokescategory);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JokesCategory jokescategory = db.JokesCategorys.Find(id);
            if (jokescategory == null)
            {
                return HttpNotFound();
            }
            return View(jokescategory);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JokesCategory jokescategory = db.JokesCategorys.Find(id);
            db.JokesCategorys.Remove(jokescategory);
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
