using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Filters;
using PersonalBlog.Areas.Admin.Models;
using PersonalBlog.Areas.Data;

namespace PersonalBlog.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private PersonalDbContext db = new PersonalDbContext();

        // GET: Admin/News
        [SessionCheck]

        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: Admin/News/Details/5
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
        [SessionCheck]

        // GET: Admin/News/Create
        public ActionResult Create()
        {
       
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news,HttpPostedFileBase AImage)
        {
            if (ModelState.IsValid)
            {
                if (AImage != null && AImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AImage.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(AImage.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name;
                    try
                    {
                        name = db.News.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                    }
                    catch
                    {
                        name = 1;
                    }
                    string firstpath1 = "/News/";
                    string secondpath = "/News/" + name + "/";
                    bool exists1 = System.IO.Directory.Exists(Server.MapPath(firstpath1));
                    bool exists2 = System.IO.Directory.Exists(Server.MapPath(secondpath));
                    if (!exists1)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath1));

                    }
                    if (!exists2)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(secondpath));

                    }
                    var path = Server.MapPath("/News/" + name + "/" + fileName1 + ext);
                    AImage.SaveAs(path);
                    news.Image = "/News/" + name + "/" + fileName1 + ext;
                }
                news.UplodedBy = Session["userEmail"].ToString();
                news.UplodedAt = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }
        [SessionCheck]

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
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
        [SessionCheck]

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateInput(false)]

        [ValidateAntiForgeryToken]
        public ActionResult Edit( News news, HttpPostedFileBase AImage)
        {
            if (ModelState.IsValid)
            {
                if (AImage != null && AImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AImage.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(AImage.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name=news.Id;
                    
                    string firstpath1 = "/News/";
                    string secondpath = "/News/" + name + "/";
                    bool exists1 = System.IO.Directory.Exists(Server.MapPath(firstpath1));
                    bool exists2 = System.IO.Directory.Exists(Server.MapPath(secondpath));
                    if (!exists1)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath1));

                    }
                    if (!exists2)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(secondpath));

                    }
                    var path = Server.MapPath("/News/" + name + "/" + fileName1 + ext);
                    AImage.SaveAs(path);
                    news.Image = "/News/" + name + "/" + fileName1 + ext;
                }
                else
                {
                    string imagename = db.News.Where(m => m.Id == news.Id).FirstOrDefault().Image;
                    news.Image = imagename;
                }
               
                 news.updatedBy = Session["userEmail"].ToString();
                news.UpdatedAt = DateTime.Now.ToShortDateString();
                News a = db.News.Find(news.Id);
                a.Image = news.Image;
                a.LongDescription = news.LongDescription;
                a.shortDetail = news.shortDetail;
                a.PublishingDate = news.PublishingDate;
                a.UpdatedAt = news.UpdatedAt;
                a.updatedBy = news.updatedBy;
                a.UplodedAt = news.UplodedAt;
                a.UplodedBy = news.UplodedBy;
                a.Title = news.Title;
                a.Category = news.Category;
                a.Id = news.Id;

               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
       

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
