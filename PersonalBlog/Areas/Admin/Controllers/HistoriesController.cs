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
    public class HistoriesController : Controller
    {
        private PersonalDbContext db = new PersonalDbContext();
        [SessionCheck]

        // GET: Admin/Histories
        public ActionResult Index()
        {
            return View(db.Histories.ToList());
        }

        // GET: Admin/Histories/Details/5
        public ActionResult Details(int? id)
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
        
        [SessionCheck]

        // GET: Admin/Histories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Histories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( History history,HttpPostedFileBase AImage)
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
                        name = db.Histories.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                    }
                    catch
                    {
                        name = 1;
                    }
                    string firstpath1 = "/History/";
                    string secondpath = "/History/" + name + "/";
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
                    var path = Server.MapPath("/History/" + name + "/" + fileName1 + ext);
                    AImage.SaveAs(path);
                    history.Image = "/History/" + name + "/" + fileName1 + ext;
                }
                history.UplodedAt = DateTime.Now.ToShortDateString();

                history.UplodedBy = Session["userEmail"].ToString();
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(history);
        }
        [SessionCheck]

        // GET: Admin/Histories/Edit/5
        public ActionResult Edit(int? id)
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
        [SessionCheck]


        // POST: Admin/Histories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( History history, HttpPostedFileBase AImage)
        {
            if (ModelState.IsValid)
            {
                if (AImage != null && AImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AImage.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(AImage.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name=history.Id;
                   
                    string firstpath1 = "/History/";
                    string secondpath = "/History/" + name + "/";
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
                    var path = Server.MapPath("/History/" + name + "/" + fileName1 + ext);
                    AImage.SaveAs(path);
                    history.Image = "/History/" + name + "/" + fileName1 + ext;
                }
                history.UpdatedAt = DateTime.Now.ToShortDateString();
                history.updatedBy = Session["userEmail"].ToString();
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
        }

        // GET: Admin/Histories/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
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
