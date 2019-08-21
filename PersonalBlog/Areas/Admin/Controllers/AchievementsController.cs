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
    [SessionCheck]
    public class AchievementsController : Controller
    {
        private PersonalDbContext db = new PersonalDbContext();

        // GET: Admin/Achievements
        public ActionResult Index()
        {
            return View(db.Achievements.ToList());
        }

        // GET: Admin/Achievements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // GET: Admin/Achievements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Achievements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,description,Image")] Achievement achievement,HttpPostedFileBase AImage)
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
                            name = db.Achievements.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                        }
                        catch
                        {
                            name = 1;
                        }
                        string firstpath1 = "/Achievements/";
                        string secondpath = "/Achievements/" + name + "/";
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
                        var path = Server.MapPath("/Achievements/" + name + "/" + fileName1 + ext);
                        AImage.SaveAs(path);
                        achievement.Image = "/Achievements/" + name + "/" + fileName1 + ext;
                    }
                achievement.UplodedAt = DateTime.Now.ToShortDateString();
                achievement.UplodedBy = Session["userEmail"].ToString();
                db.Achievements.Add(achievement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(achievement);
        }

        // GET: Admin/Achievements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Admin/Achievements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,description,Image")] Achievement achievement, HttpPostedFileBase AImage)
        {
            if (ModelState.IsValid)
            {
                if (AImage != null && AImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AImage.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(AImage.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name=achievement.Id;
                   
                    string firstpath1 = "/Achievements/";
                    string secondpath = "/Achievements/" + name + "/";
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
                    var path = Server.MapPath("/Achievements/" + name + "/" + fileName1 + ext);
                    AImage.SaveAs(path);
                    achievement.Image = "/Achievements/" + name + "/" + fileName1 + ext;
                }
                achievement.UpdatedAt = DateTime.Now.ToShortDateString();
                achievement.updatedBy = Session["userEmail"].ToString();
                db.Entry(achievement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(achievement);
        }

        // GET: Admin/Achievements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.Find(id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: Admin/Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achievement achievement = db.Achievements.Find(id);
            db.Achievements.Remove(achievement);
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
