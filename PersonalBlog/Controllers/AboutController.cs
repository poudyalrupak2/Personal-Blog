using PersonalBlog.Areas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        PersonalDbContext db = new PersonalDbContext();

        public ActionResult Index()
        {
            return View(db.News.Where(m=>m.Category=="news").ToList());
        }
    }
}