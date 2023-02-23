using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Controllers
{

    [AllowAnonymous]

    public class PostController : Controller
    {

        Context db = new Context();

        [HttpGet]
        public ActionResult PostDetails(int id, Comment comment)
        {
            TempData["Status"] = "display: none;";

            ViewBag.CommentCount = db.Comments.Where(x => x.PostID == id && x.Status == true).Count().ToString();

            var value = db.Posts.Find(id).ClickCount;
            int clickCount = value;
            clickCount++;

            db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult PostDetails(Comment comment, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "display: inline;";
                return View("PostDetails");
            }

            comment.PostID = id;
            comment.Date = DateTime.Now;
            comment.Status = true;

            TempData["Result"] = "Message Sent!";

            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("PostDetails");
        }

        public PartialViewResult Content(int id)
        {
            var values = db.Posts.Where(x => x.ID == id && x.Status == true).ToList();

            ViewBag.CommentCount = db.Comments.Where(x => x.PostID == id && x.Status == true).Count().ToString();
            ViewBag.CategoryName = db.Posts.Where(x => x.ID == id).FirstOrDefault().Category.Name;

            var value = db.Posts.Find(id).ClickCount;
            int clickCount = value;
            clickCount++;

            db.SaveChanges();
            return PartialView(values);
        }

        public PartialViewResult CommentList(int id)
        {
            var values = db.Comments.Where(x => x.PostID == id && x.Status == true).ToList();
            return PartialView(values);
        }

        public PartialViewResult PopularPostsByCategory(int id)
        {
            var categoryId = db.Posts.Where(x => x.ID == id).FirstOrDefault().Category.ID;

            ViewBag.CategoryName = db.Posts.Find(id).Category.Name.ToString();

            var values = db.Posts.Where(x => x.CategoryID == categoryId && x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ClickCount).Take(5).ToList();
            return PartialView(values);
        }

    }
}