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

    public class GalleriesController : Controller
    {
        private PersonalDbContext db = new PersonalDbContext();

        // GET: Admin/Galleries
        public ActionResult Index()
        {
            return View(db.Gallery.ToList());
        }

        // GET: Admin/Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Admin/Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery gallery,HttpPostedFileBase Thumbnail,List<HttpPostedFileBase> Images)
        {
            if (Thumbnail!=null && Thumbnail.ContentLength>0)
                {
                    var fileName = Path.GetFileName(Thumbnail.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(Thumbnail.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name;
                    try
                    {
                        name = db.Gallery.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                    name = name + 1;
                    }
                    catch
                    {
                        name = 1;
                    }
                    string firstpath1 = "/Gallery/";
                    string secondpath = "/Gallery/" + name + "/";
                    string ThirdPath = "/Gallery/" + name + "/Thumbnail/";
                    bool exists1 = System.IO.Directory.Exists(Server.MapPath(firstpath1));
                    bool exists2 = System.IO.Directory.Exists(Server.MapPath(secondpath));
                    bool exists3 = System.IO.Directory.Exists(Server.MapPath(ThirdPath));

                    if (!exists1)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath1));

                    }
                    if (!exists2)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(secondpath));

                    }

                    if (!exists3)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(ThirdPath));

                    }
                    var path = Server.MapPath("/Gallery/" + name + "/Thumbnail/"+ fileName1 + ext);
                    Thumbnail.SaveAs(path);
                    gallery.Imagepath = "/Gallery/" + name + "/Thumbnail/" + fileName1 + ext;
                }
                List<Images> im = new List<Images>();
                if (Images.Count > 0)
                {
                    foreach (var images in Images)
                    {
                        if (images.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(images.FileName);
                            var fileName1 = Path.GetFileNameWithoutExtension(images.FileName);

                            // extract only the fielname
                            var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                            int name;
                            try
                            {
                                name = db.Gallery.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                            name = name + 1;
                            }
                            catch
                            {
                                name = 1;
                            }
                            string firstpath1 = "/Gallery/";
                            string secondpath = "/Gallery/" + name + "/";
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

                     
                            var path = Server.MapPath("/Gallery/" + name +"/"+ fileName1 + ext);
                            Images img = new Images();
                            img.FilePath = "/Gallery/" + name + "/" + fileName1 + ext;
                            img.Name = images.FileName;
                            im.Add(img);
                            images.SaveAs(path);
                        }
                    }
                }
            gallery.Images = im;
            db.Gallery.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            

        }

        // GET: Admin/Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Gallery gallery, HttpPostedFileBase Thumbnail, List<HttpPostedFileBase> Images)
        {
       




                if (Thumbnail != null && Thumbnail.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Thumbnail.FileName);
                    var fileName1 = Path.GetFileNameWithoutExtension(Thumbnail.FileName);

                    // extract only the fielname
                    var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                    int name=gallery.Id;
                   
                    string firstpath1 = "/Gallery/";
                    string secondpath = "/Gallery/" + name + "/";
                    string ThirdPath = "/Gallery/" + name + "/Thumbnail/";
                    bool exists1 = System.IO.Directory.Exists(Server.MapPath(firstpath1));
                    bool exists2 = System.IO.Directory.Exists(Server.MapPath(secondpath));
                    bool exists3 = System.IO.Directory.Exists(Server.MapPath(ThirdPath));

                    if (!exists1)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(firstpath1));

                    }
                    if (!exists2)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(secondpath));

                    }

                    if (!exists3)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(ThirdPath));

                    }
                    var path = Server.MapPath("/Gallery/" + name + "/Thumbnail/" + fileName1 + ext);
                    Thumbnail.SaveAs(path);
                    gallery.Imagepath = "/Gallery/" + name + "/Thumbnail/" + fileName1 + ext;
                }
            var model = db.Gallery.Include(t => t.Images).SingleOrDefault(x => x.Id == gallery.Id);
            List<Images> im = new List<Images>();
                if (Images.Count > 0)
                {
                if (Images.Any(m => m.ContentLength > 0))
                {
                    foreach (var images in model.Images)
                    {
                        string imagepath = images.FilePath.ToString();
                        var fileName =Server.MapPath(imagepath);
                        if(System.IO.File.Exists(fileName))
                           {
                            System.IO.File.Delete(fileName);
                        }
                        db.Database.ExecuteSqlCommand("delete from Images where FilePath='" + fileName + "'");

                    }
                }
               }
            foreach (var images in Images)
                    {
                        if (images != null)
                        {
                            if (images.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(images.FileName);
                                var fileName1 = Path.GetFileNameWithoutExtension(images.FileName);

                                // extract only the fielname
                                var ext = Path.GetExtension(fileName.ToLower());            //extract only the extension of filename and then convert it into lower case.


                                int name = gallery.Id;

                                string firstpath1 = "/Gallery/";
                                string secondpath = "/Gallery/" + name + "/";
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


                                var path = Server.MapPath("/Gallery/" + name + "/" + fileName1 + ext);
                                Images img = new Images();
                                img.FilePath = "/Gallery/" + name + "/" + fileName1 + ext;
                                img.Name = images.FileName;
                            images.SaveAs(path);

                        Images image = new Images()
                        {
                            FilePath = "/Gallery/" + name + "/" + fileName1 + ext,
                            Name=fileName1,
                            

                        };
                      
                        im.Add(image);
                            }
                        

                    }
                }
                 gallery.Images = im;
            Gallery gal = db.Gallery.Where(m => m.Id == gallery.Id).FirstOrDefault();
            gal.Name = gallery.Name;
            gal.Images = gallery.Images;
            gal.Imagepath = gallery.Imagepath;
            gal.Caption = gallery.Caption;
                db.SaveChanges();
                return RedirectToAction("Index");

        }

        // GET: Admin/Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Gallery.Find(id);
            List<Images> im = new List<Images>();
            var model = db.Gallery.Include(t => t.Images).SingleOrDefault(x => x.Id == gallery.Id);
            foreach (var images in model.Images)
            {
                string imagepath = images.FilePath.ToString();
                var fileName = Server.MapPath(imagepath);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
                db.Database.ExecuteSqlCommand("delete from Images where FilePath='" + fileName + "'");

            }
            db.Gallery.Remove(model);

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
