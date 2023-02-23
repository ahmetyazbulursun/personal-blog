using personal_blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Controllers
{

    [AllowAnonymous]

    public class ContactController : Controller
    {

        Context db = new Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Message message)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            message.Date = DateTime.Now;
            message.Status = true;

            TempData["Result"] = "Message Sent!";

            db.Messages.Add(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}