using System.Linq;
using System.Web.Mvc;
using OnlineJokes.Context;
using OnlineJokes.Models;

namespace OnlineJokes.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        private readonly JokesContext db = new JokesContext();

        public ActionResult AdminRegistration()
        {
            ViewBag.AdminRegistration = "active";
            if (Session["AdminId"] != null)
            {
                return RedirectToAction("JokesIndex", "Jokes");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegistration(Admin admin)
        {
            ViewBag.AdminRegistration = "active";
            var k = db.Admins.Count(r => r.Email == admin.Email);
            if (k == 0)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Another Admin ID is Registered with this Email";
                return View();
            }

            return RedirectToAction("LogIn");
        }

        public ActionResult LogIn()
        {
            ViewBag.LogIn = "active";
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Admin admin)
        {
            ViewBag.LogIn = "active";
            var password = admin.Password;
            var email = admin.Email;
            var p = db.Admins.Where(c => c.Email == email && c.Password == password)
                    .Select(c => new {c.Id, c.Email})
                    .ToList();
            if (p.Any())
            {
                foreach (var k in p)
                {
                    Session["AdminId"] = k.Id;
                    Session["AdminEmail"] = k.Email;
                }
                return RedirectToAction("JokesIndex", "Jokes");
            }
            else
            {
                ViewBag.Error = "Login Failed";
            }
            return View();
        }

        public ActionResult AdminLogout()
        {
            Session["AdminId"] = null;
            Session["AdminEmail"] = null;
            return RedirectToAction("LogIn", "Authentication");
        }
    }
}