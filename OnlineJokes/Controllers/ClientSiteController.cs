using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OnlineJokes.Context;
using OnlineJokes.Models;

namespace OnlineJokes.Controllers
{
    public class ClientSiteController : Controller
    {
        public object ClientSite { get; private set; }

        //
        // GET: /ClientSite/
        public ActionResult Home()
        {
            List<JokesCategory> jokesCategorys;
            List<JokesInfo> jokesInfos;

            using (var db = new JokesContext())
            {
                jokesCategorys = db.JokesCategorys.ToList();
                jokesInfos = db.JokesInfos.ToList();
            }

            ViewBag.jokesCategory = jokesCategorys;
            ViewBag.jokesInfo = jokesInfos;
            return View();
        }
        public JsonResult GetAllJokesbyCategoryId(int categoryid)
        {
            List<JokesInfo> jokesInfos = new List<JokesInfo>();
            List<GetAllJokeInfoByCategory> getjokesInfos = new List<GetAllJokeInfoByCategory>();
            string urlLink = "";
            using (var db = new JokesContext())
            {
                string url = HttpContext.Request.Url.AbsoluteUri;
                int position = url.IndexOf("GetAllJokesbyCategoryId");

                if (position >= 0)
                {
                     urlLink = url.Remove(position);
                }

                if (categoryid == 0)
                {
                    jokesInfos = db.JokesInfos.ToList();
                    foreach(var item in jokesInfos)
                    {
                        GetAllJokeInfoByCategory joke = new GetAllJokeInfoByCategory();
                        joke.WriterName = item.WriterName;
                        joke.JokesName = item.JokesName;
                        joke.Image = item.Image;
                        joke.Id = item.Id;
                        joke.UrlLink = urlLink;
                        getjokesInfos.Add(joke);
                    }
                }
                else
                {
                    var jokes = db.JokesInfos.Where(s => s.JokesCategoryId == categoryid);
                    foreach (var item in jokes)
                    {
                        GetAllJokeInfoByCategory joke = new GetAllJokeInfoByCategory();
                        joke.WriterName = item.WriterName;
                        joke.JokesName = item.JokesName;
                        joke.Image = item.Image;
                        joke.Id = item.Id;
                        joke.UrlLink = urlLink;
                        getjokesInfos.Add(joke);
                    }
                }

            }
            return Json(getjokesInfos);
        }

        public ActionResult JokeShow(int id)
        {
            ViewJokesInfo viewjoke = new ViewJokesInfo();
            List<ClientComment> clientComments = new List<ClientComment>();
            using (var db = new JokesContext())
            {
                var jokes = db.JokesInfos.FirstOrDefault(s => s.Id == id);
                viewjoke.WriterName = jokes.WriterName;
                viewjoke.JokesName = jokes.JokesName;
                viewjoke.Image = jokes.Image;
                viewjoke.Id = jokes.Id;
                List<string> str = new List<string>( jokes.Jokes.Split(new string[] { "\n" }, StringSplitOptions.None));
                viewjoke.Jokes = str;

                var comments = db.ClientComments.Where(i => i.JokesInfoId == id);
                if(comments != null)
                {
                    foreach (var item in comments)
                    {
                        ClientComment comment = new ClientComment();
                        comment.CommentDateTime = item.CommentDateTime;
                        comment.Comment = item.Comment;
                        clientComments.Add(comment);
                    }
                   
                }
                
            }
            ViewBag.jokesInfo = viewjoke;
            ViewBag.Comment = clientComments;
            return View();
        }
        [HttpPost]
        public ActionResult JokeShow(ClientComment clientComment)
        {
            ViewJokesInfo viewjoke = new ViewJokesInfo();
            List<ClientComment> clientComments = new List<ClientComment>();
            int jokesId;
            using (var db = new JokesContext())
            {
                clientComment.CommentDateTime = System.DateTime.Now.ToLocalTime();
                db.ClientComments.Add(clientComment);
                db.SaveChanges();

                var jokes = db.JokesInfos.FirstOrDefault(s => s.Id == clientComment.JokesInfoId);
                viewjoke.WriterName = jokes.WriterName;
                viewjoke.JokesName = jokes.JokesName;
                viewjoke.Image = jokes.Image;
                viewjoke.Id = jokes.Id;
                jokesId = jokes.Id;
                List<string> str = new List<string>(jokes.Jokes.Split(new string[] { "\n" }, StringSplitOptions.None));
                viewjoke.Jokes = str;

                var comments = db.ClientComments.Where(i => i.JokesInfoId == clientComment.JokesInfoId);
                if (comments != null)
                {
                    foreach (var item in comments)
                    {
                        ClientComment comment = new ClientComment();
                        comment.CommentDateTime = item.CommentDateTime;
                        comment.Comment = item.Comment;
                        clientComments.Add(comment);
                    }

                }
            }
            ViewBag.jokesInfo = viewjoke;
            ViewBag.Comment = clientComments;
            return RedirectToAction("JokeShow", new RouteValueDictionary(
            new { controller = ClientSite, action = "JokeShow", Id = jokesId }));
        }
    }
}