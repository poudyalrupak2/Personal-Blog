using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HotelManagemant.Filters;
using PersonalBlog.Areas.Admin.Models;
using PersonalBlog.Areas.Data;

namespace PersonalBlog.Areas.Admin.Controllers
{
    [SessionCheck]

    public class AdminController : Controller
    {
        private PersonalDbContext db = new PersonalDbContext();

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View(db.PersonalDetails.ToList());
        }

        // GET: Admin/Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDetail personalDetail = db.PersonalDetails.Find(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // GET: Admin/Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ShortDetail,LongDescription,Email,Image")] PersonalDetail personalDetail,HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                
                    int data = db.login.Where(t => t.Email == personalDetail.Email).Count();
                    if (data > 0)
                    {
                        ModelState.AddModelError("", "Email already exists");
                        return View();
                    }
                    Random generator = new Random();
                    String password = generator.Next(0, 999999).ToString("D6");



                    var message = new MailMessage();
                    message.To.Add(new MailAddress(personalDetail.Email));
                    message.Subject = "Account has been created";
                    message.Body = "Use this Password to login:" + password;
                    using (var smtp = new SmtpClient())
                    {
                        try
                        {

                            smtp.Send(message);
                            db.login.Add(new Login
                            {
                                Email = personalDetail.Email,
                                Role = "Admin",
                                RandomPass = password,


                            });

                            TempData["Message"] = "Admin Created Successfully.";


                        }
                        catch (Exception e)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.RequestTimeout);
                        }
                    }
                    if (files != null && files.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(files.FileName);
                        var fileName1 = Path.GetFileNameWithoutExtension(files.FileName);

                        // extract only the fielname
                        var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                        int name;
                        try
                        {
                            name = db.PersonalDetails.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                        }
                        catch
                        {
                            name = 1;
                        }
                        string firstpath1 = "/Adminp/";
                        string secondpath = "/Adminp/" + name + "/";
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
                        var path = Server.MapPath("/Adminp/" + name + "/" + fileName1 + ext);
                    files.SaveAs(path);
                        personalDetail.Image = "/Adminp/" + name + "/" + fileName1 + ext;
                    }
                        db.PersonalDetails.Add(personalDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            return View(personalDetail);

             
        }

        // GET: Admin/Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDetail personalDetail = db.PersonalDetails.Find(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // POST: Admin/Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ShortDetail,LongDescription,Email,Image")] PersonalDetail personalDetail, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                if(files!=null&&files.ContentLength>0)
                {
                    var fileName = Path.GetFileName(files.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(files.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name;
                    name = personalDetail.Id;
                 
                    string firstpath1 = "/Admin/";
                    string secondpath = "/Admin/" + name + "/";
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
                    var path = Server.MapPath("/Admin/" + name + "/" + fileName1 + ext);
                    files.SaveAs(path);
                    personalDetail.Image = "/Admin/" + name + "/" + fileName1 + ext;

                }
                db.Entry(personalDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalDetail);
        }

        // GET: Admin/Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDetail personalDetail = db.PersonalDetails.Find(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // POST: Admin/Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalDetail personalDetail = db.PersonalDetails.Find(id);
            db.PersonalDetails.Remove(personalDetail);
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
