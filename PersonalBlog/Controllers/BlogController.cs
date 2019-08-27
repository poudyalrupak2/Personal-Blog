using PersonalBlog.Areas.Admin.Models;
using PersonalBlog.Areas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Controllers
{
    public class BlogController : Controller
    {
        PersonalDbContext db = new PersonalDbContext();
        public ActionResult Index()
        {

            return View(db.News.Where(m=>m.Category=="Event"));
        }
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
    }
}