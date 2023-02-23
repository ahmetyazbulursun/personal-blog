using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace personal_blog.Controllers
{

    [AllowAnonymous]

    public class LoginController : Controller
    {

        Context db = new Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            var values = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);

            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.Username, false);
                Session["UserId"] = values.ID.ToString();
                return RedirectToAction("Index", "Admin");
            }

            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}