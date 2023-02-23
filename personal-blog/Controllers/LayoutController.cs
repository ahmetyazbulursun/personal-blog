using PagedList;
using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Controllers
{

    [AllowAnonymous]

    public class LayoutController : Controller
    {

        Context db = new Context();

        public PartialViewResult Carousel()
        {
            var values = db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderByDescending(x => x.ID).Take(3).ToList();
            return PartialView(values);
        }

        public PartialViewResult NavbarCategories()
        {
            var values = db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name).ToList();
            return PartialView(values);
        }

        public PartialViewResult NavbarAccounts()
        {
            var values = db.Accounts.Where(x => x.Status == true && x.Icon.Status == true).ToList();
            return PartialView(values);
        }

        public PartialViewResult FooterAboutUs()
        {
            var values = db.Abouts.ToList();
            return PartialView(values);
        }

        public PartialViewResult FooterLatestPosts()
        {
            var values = db.Posts.Where(x => x.Status == true && x.Category.Status == true).OrderBy(x => x.ID).Take(3).ToList();
            return PartialView(values);
        }

        public PartialViewResult FooterAccounts()
        {
            var values = db.Accounts.Where(x => x.Status == true && x.Icon.Status == true).ToList();
            return PartialView(values);
        }

    }
}