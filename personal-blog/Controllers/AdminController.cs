using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Controllers
{
    public class AdminController : Controller
    {

        Context db = new Context();

        // Index Page
        public ActionResult Index()
        {
            ViewBag.Posts = db.Posts.Where(x => x.Status == true && x.Category.Status == true).Count().ToString();
            ViewBag.UnreadMessages = db.Messages.Where(x => x.Status == true).Count().ToString();
            ViewBag.Categories = db.Categories.Where(x => x.Status == true).Count().ToString();
            ViewBag.Accounts = db.Accounts.Where(x => x.Status == true && x.Icon.Status == true).Count().ToString();

            return View();
        }

        public PartialViewResult LatestPosts()
        {
            var values = db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ID).Take(3).ToList();
            return PartialView(values);
        }

        // Posts Page
        public ActionResult Posts()
        {
            var values = db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ID).ToList();
            return View(values);
        }

        // New Post Page
        // ---------------------------------------------
        [HttpGet]
        public ActionResult NewPost()
        {
            List<SelectListItem> category = (from x in db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name)
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.ID.ToString()
                                             }).ToList();

            ViewBag.Category = category;

            return View();
        }

        [HttpPost]
        public ActionResult NewPost(Post post, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> category = (from x in db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name)
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.ID.ToString()
                                                 }).ToList();

                ViewBag.Category = category;

                return View("NewPost");
            }

            string postImageFile = "~/Images/Posts";
            bool exists = System.IO.Directory.Exists(Server.MapPath(postImageFile));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(postImageFile));
            }

            try
            {
                if (Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = ("~/Images/Posts/" + dateTime + fileName).Replace(" ", "-");
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    post.Image = ("/Images/Posts/" + dateTime + fileName).Replace(" ", "-");

                }
            }
            catch (Exception)
            {
                post.Image = "/Templates/admin-template/default-img.jpg";
            }

            post.AuthorID = db.Authors.FirstOrDefault().ID;
            post.Status = true;
            post.Date = DateTime.Now;
            post.ClickCount = 0;

            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Posts");
        }
        // ---------------------------------------------

        // Post Details Page
        // ---------------------------------------------
        public ActionResult PostDetails(int id)
        {
            var values = db.Posts.Where(x => x.ID == id).ToList();
            return View(values);
        }

        // Post Update Page
        // ---------------------------------------------
        [HttpGet]
        public ActionResult EditPost(int id)
        {
            var values = db.Posts.Find(id);

            List<SelectListItem> category = (from x in db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name)
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.ID.ToString()
                                             }).ToList();

            ViewBag.Category = category;

            return View(values);
        }

        [HttpPost]
        public ActionResult EditPost(Post post, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> category = (from x in db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name)
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.ID.ToString()
                                                 }).ToList();

                ViewBag.Category = category;

                return View("UpdatePost");
            }

            var values = db.Posts.Find(post.ID);

            try
            {
                if (Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = ("~/Images/Posts/" + dateTime + fileName).Replace(" ", "-");
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    values.Image = ("/Images/Posts/" + dateTime + fileName).Replace(" ", "-");

                }
            }
            catch (Exception)
            {
                values.Image = values.Image;
            }


            values.Header = post.Header;
            values.Details = post.Details;
            values.CategoryID = post.CategoryID;

            db.SaveChanges();
            return RedirectToAction("Posts");
        }
        // ---------------------------------------------

        // Comments By Posts
        // ---------------------------------------------
        public ActionResult DeletePost(int id)
        {
            var values = db.Posts.Find(id);

            values.Status = false;

            db.SaveChanges();
            return RedirectToAction("Posts");
        }

        // Comments By Posts Page
        // -----------s----------------------------------
        public ActionResult Comments(int id)
        {
            var values = db.Comments.Where(x => x.PostID == id && x.Status == true).OrderByDescending(x => x.ID).ToList();
            return View(values);
        }
        // ---------------------------------------------

        // All Comments Page
        // ---------------------------------------------
        public ActionResult AllComments()
        {
            var values = db.Comments.Where(x => x.Status == true && x.Post.Status == true).OrderByDescending(x => x.ID).ToList();
            return View(values);
        }
        // ---------------------------------------------

        // Delete Comment Operation
        // ---------------------------------------------
        public ActionResult DeleteComment(Comment comment)
        {
            var values = db.Comments.Find(comment.ID);

            values.Status = false;

            db.SaveChanges();
            return RedirectToAction("AllComments");
        }
        // ---------------------------------------------

        // Categories Page
        // ---------------------------------------------
        public ActionResult Categories()
        {
            var values = db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name).ToList();
            return View(values);
        }
        // ---------------------------------------------

        // New Category Operation
        // ---------------------------------------------
        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCategory");
            }

            category.Status = true;

            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        // ---------------------------------------------

        // Category Delete Operation
        // ---------------------------------------------
        public ActionResult DeleteCategory(Category category)
        {
            var values = db.Categories.Find(category.ID);

            values.Status = false;

            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        // ---------------------------------------------

        // Category Edit Operation
        // ---------------------------------------------

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var values = db.Categories.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCategory");
            }

            var values = db.Categories.Find(category.ID);

            values.Name = category.Name;

            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        // ---------------------------------------------

        // Category Edit Operation
        // ---------------------------------------------
        public ActionResult PostsByCategory(int id)
        {
            var values = db.Posts.Where(x => x.CategoryID == id && x.Status == true).OrderByDescending(x => x.ID).ToList();

            ViewBag.CategoryName = db.Posts.Find(id).Category.Name;

            return View(values);
        }
        // ---------------------------------------------

        // About Page
        // ---------------------------------------------
        public ActionResult About()
        {
            var values = db.Abouts.ToList();
            return View(values);
        }
        // ---------------------------------------------

        // Edit About Page
        // ---------------------------------------------
        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            var values = db.Abouts.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult EditAbout(About about, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                return View("EditAbout");
            }

            string aboutImageFile = "~/Images/About";
            bool exists = System.IO.Directory.Exists(Server.MapPath(aboutImageFile));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(aboutImageFile));
            }

            var values = db.Abouts.Find(about.ID);

            try
            {
                if (Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = ("~/Images/About/" + dateTime + fileName).Replace(" ", "-");
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    values.Image = ("/Images/About/" + dateTime + fileName).Replace(" ", "-");

                }
            }
            catch (Exception)
            {
                values.Image = values.Image;
            }

            values.Header = about.Header;
            values.Details = about.Details;

            db.SaveChanges();
            return RedirectToAction("About");
        }
        // ---------------------------------------------

        // Author Page
        // ---------------------------------------------
        public ActionResult Author()
        {
            var values = db.Authors.ToList();
            return View(values);
        }
        // ---------------------------------------------

        // Author Update
        // ---------------------------------------------
        [HttpGet]
        public ActionResult EditAuthor(int id)
        {
            var values = db.Authors.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult EditAuthor(Author author, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                return View("EditAuthor");
            }

            string authorImageFile = "~/Images/Author";
            bool exists = System.IO.Directory.Exists(Server.MapPath(authorImageFile));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(authorImageFile));
            }

            var values = db.Authors.Find(author.ID);

            try
            {
                if (Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = ("~/Images/Author/" + dateTime + fileName).Replace(" ", "-");
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    values.Image = ("/Images/Author/" + dateTime + fileName).Replace(" ", "-");

                }
            }
            catch (Exception)
            {
                values.Image = values.Image;
            }

            values.Fullname = author.Fullname;
            values.Details = author.Details;

            db.SaveChanges();
            return RedirectToAction("Author");
        }
        // ---------------------------------------------

        // Author Update
        // ---------------------------------------------
        public ActionResult Messages()
        {
            var values = db.Messages.Where(x => x.Status == true).OrderByDescending(x => x.ID).ToList();
            return View(values);
        }
        // ---------------------------------------------

        // Author Update
        // ---------------------------------------------
        public ActionResult SaveAsRead(int id)
        {
            var values = db.Messages.Find(id);

            values.Status = false;

            db.SaveChanges();
            return RedirectToAction("Messages");
        }
        // ---------------------------------------------

        // Accounts Page
        // ---------------------------------------------
        public ActionResult Accounts()
        {
            var values = db.Accounts.Where(x => x.Status == true).ToList();
            return View(values);
        }
        // ---------------------------------------------

        // Accounts Page
        // ---------------------------------------------
        [HttpGet]
        public ActionResult NewAccount()
        {
            List<SelectListItem> icon = (from x in db.Icons.Where(x => x.Status == true).OrderBy(x => x.IconName)
                                         select new SelectListItem
                                         {
                                             Text = x.IconName,
                                             Value = x.ID.ToString()
                                         }).ToList();

            ViewBag.Icon = icon;

            return View();
        }

        [HttpPost]
        public ActionResult NewAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> icon = (from x in db.Icons.Where(x => x.Status == true).OrderBy(x => x.IconName)
                                             select new SelectListItem
                                             {
                                                 Text = x.IconName,
                                                 Value = x.ID.ToString()
                                             }).ToList();

                ViewBag.Icon = icon;

                return View("NewAccount");
            }

            account.Status = true;

            db.Accounts.Add(account);
            db.SaveChanges();
            return RedirectToAction("Accounts");
        }
        // ---------------------------------------------

        // Accounts Page
        // ---------------------------------------------
        public ActionResult DeleteAccount(int id)
        {
            var values = db.Accounts.Find(id);

            values.Status = false;

            db.SaveChanges();
            return RedirectToAction("Accounts");
        }
        // ---------------------------------------------

        // Edit Account
        // ---------------------------------------------
        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            var values = db.Accounts.Find(id);

            List<SelectListItem> icon = (from x in db.Icons.Where(x => x.Status == true).OrderBy(x => x.IconName)
                                         select new SelectListItem
                                         {
                                             Text = x.IconName,
                                             Value = x.ID.ToString()
                                         }).ToList();

            ViewBag.Icon = icon;

            return View(values);
        }

        [HttpPost]
        public ActionResult EditAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> icon = (from x in db.Icons.Where(x => x.Status == true).OrderBy(x => x.IconName)
                                             select new SelectListItem
                                             {
                                                 Text = x.IconName,
                                                 Value = x.ID.ToString()
                                             }).ToList();

                ViewBag.Icon = icon;

                return View("EditAccount");
            }

            var values = db.Accounts.Find(account.ID);

            values.Link = account.Link;
            values.IconID = account.IconID;

            db.SaveChanges();
            return RedirectToAction("Accounts");
        }
        // ---------------------------------------------

        // User Page
        // ---------------------------------------------
        public ActionResult UserDetails()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var values = db.Users.Where(x => x.ID == userId).ToList();
            return View(values);
        }
        // ---------------------------------------------

        // User Page
        // ---------------------------------------------
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var values = db.Users.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View("EditUser");
            }

            var values = db.Users.Find(user.ID);

            values.Username = user.Username;
            values.Password = user.Password;

            db.SaveChanges();
            return RedirectToAction("UserDetails");
        }

    }
}