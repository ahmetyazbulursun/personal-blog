using PagedList;
using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Controllers
{

    [AllowAnonymous]

    public class CategoryController : Controller
    {

        Context db = new Context();

        public ActionResult Posts(int id, int page = 1)
        {
            var values = db.Posts.Where(x => x.CategoryID == id && x.Status == true).ToList().ToPagedList(page, 8);

            ViewBag.CategoryName = db.Categories.Find(id).Name;

            return View(values);
        }
    }
}