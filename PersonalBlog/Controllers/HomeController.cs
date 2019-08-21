using PersonalBlog.Areas.Admin.Models;
using PersonalBlog.Areas.Data;
using PersonalBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        PersonalDbContext db = new PersonalDbContext();

        public ActionResult Index()
        {
            IndexViewModel indexs= new IndexViewModel();
          
            indexs.news = db.News.Where(m=>m.Category!= "SportNews" && m.PublishingDate<DateTime.Now).Take(5).OrderByDescending(m=>m.Id).ToList();
            indexs.SportNews = db.News.Where(m => m.Category == "SportNews" && m.PublishingDate < DateTime.Now).Take(5).OrderByDescending(m => m.Id).ToList();
            indexs.Acheivement = db.Achievements.OrderByDescending(m => m.Id).Take(5).ToList();
            indexs.galleries = db.Gallery.ToList();
            indexs.histories = db.Histories.OrderByDescending(m => m.Id).ToList();
            return View(indexs);
        }
        public ActionResult Details (int id)
        {
            Gallery gal = new Gallery();
            gal = db.Gallery.Find(id);
            List<Images> im = new List<Images>();
            im = db.Images.Where(m => m.Gallery.Id == id).ToList();
            gal.Images = im;

            return View(gal);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }
        public ActionResult NDetails(int? id)
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
        public ActionResult Gallery()
        {
            return View(db.Gallery.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(string fname,string email,string subject, string country)
        {
            return View();
            //var message = new MailMessage();
            //message.To.Add(new MailAddress(personalDetail.Email));
            //message.Subject = "Account has been created";
            //message.Body = "Use this Password to login:" + password;
            //using (var smtp = new SmtpClient())
            //{
            //    try
            //    {

            //        smtp.Send(message);
            //        db.login.Add(new Login
            //        {
            //            Email = personalDetail.Email,
            //            Role = "Admin",
            //            RandomPass = password,


            //        });

            //        TempData["Message"] = "Admin Created Successfully.";


            //    }
            //    catch (Exception e)
            //    {

            //        return new HttpStatusCodeResult(HttpStatusCode.RequestTimeout);
            //    }
            }
    }
}