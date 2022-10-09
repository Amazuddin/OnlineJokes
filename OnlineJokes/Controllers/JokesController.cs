using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineJokes.Models;
using OnlineJokes.Context;

namespace OnlineJokes.Controllers
{
    public class JokesController : Controller
    {
        private JokesContext db = new JokesContext();

        // GET: /Jokes/
        public ActionResult JokesIndex()
        {
            ViewBag.JokesIndex = "active";
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("LogIn", "Authentication");
            }
            return View(db.JokesInfos.ToList());
        }

        // GET: /Jokes/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("LogIn", "Authentication");
            }
            JokesInfo b = new JokesInfo();
            ViewBag.Category = db.JokesCategorys.ToList();
            return View(b);
        }

        // POST: /Jokes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JokesCategoryId,WriterName,JokesName,Jokes,Image")] JokesInfo jokesinfo, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    try
                    {
                        string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                        string uploadUrl = Server.MapPath("~/picture");
                        Image.SaveAs(Path.Combine(uploadUrl, fileName));
                        jokesinfo.Image = "picture/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }

                jokesinfo.WriterName = jokesinfo.WriterName;
                jokesinfo.JokesName = jokesinfo.JokesName;
                jokesinfo.Jokes = jokesinfo.Jokes;
                jokesinfo.JokesCategoryId = jokesinfo.JokesCategoryId;
                db.JokesInfos.Add(jokesinfo);
                db.SaveChanges();
            }

            return RedirectToAction("JokesIndex", new { message = "Jokes added Successfully" });
        }

        // GET: /Jokes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Category = db.JokesCategorys.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JokesInfo jokesinfo = db.JokesInfos.Find(id);
            if (jokesinfo == null)
            {
                return HttpNotFound();
            }
            return View(jokesinfo);
        }

        // POST: /Jokes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JokesCategoryId,WriterName,JokesName,Jokes,Image")] JokesInfo jokesinfo, HttpPostedFileBase Image, string pastImage)
        {
            jokesinfo.Image = pastImage;
            if (Image != null && Image.ContentLength > 0)
            {
                string fullPath = Request.MapPath("~/" + pastImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                try
                {
                    string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Image.FileName);
                    string uploadUrl = Server.MapPath("~/picture");
                    Image.SaveAs(Path.Combine(uploadUrl, fileName));
                    jokesinfo.Image = "picture/" + fileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "ERROR:" + ex.Message.ToString();
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(jokesinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("JokesIndex");
            }
            return View(jokesinfo);
        }

        // GET: /Jokes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JokesInfo jokesinfo = db.JokesInfos.Find(id);
            if (jokesinfo == null)
            {
                return HttpNotFound();
            }
            return View(jokesinfo);
        }

        // POST: /Jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JokesInfo jokesinfo = db.JokesInfos.Find(id);
            db.JokesInfos.Remove(jokesinfo);
            db.SaveChanges();
            return RedirectToAction("JokesIndex");
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
