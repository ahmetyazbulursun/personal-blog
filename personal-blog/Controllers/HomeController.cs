using PagedList;
using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Controllers
{

    [AllowAnonymous]

    public class HomeController : Controller
    {

        Context db = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Carousel()
        {
            var values = db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ID).Take(3).ToList();
            return PartialView(values);
        }

        public PartialViewResult LatestPost(int page = 1, string search = "")
        {
            var values = from x in db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ID) select x;
            if (!string.IsNullOrEmpty(search))
            {
                values = values.Where(x => x.Header.ToLower().Contains(search));
            }
            return PartialView(values.ToPagedList(page, 8));
        }

        public PartialViewResult Author()
        {
            var values = db.Authors.ToList();
            return PartialView(values);
        }

        public PartialViewResult AuthorAccounts()
        {
            var values = db.Accounts.Where(x => x.Status == true).ToList();
            return PartialView(values);
        }

        public PartialViewResult PopularPosts()
        {
            var values = db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ClickCount).Take(3).ToList();
            return PartialView(values);
        }

        public PartialViewResult Categories()
        {
            var values = db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name).ToList();
            return PartialView(values);
        }

    }
}