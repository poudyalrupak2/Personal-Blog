using PersonalBlog.Areas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Controllers
{
    public class PortfolioController : Controller
    {
        PersonalDbContext db = new PersonalDbContext();
        // GET: Portfolio
        public ActionResult Index()
        {
            return View(db.News.Where(m => m.Category == "SportNews" && m.PublishingDate < DateTime.Now));
           
        }
        public ActionResult SagNews()
        {
            return View(db.News.Where(m => m.Category == "SAGGame" && m.PublishingDate < DateTime.Now));

        }
    }
}