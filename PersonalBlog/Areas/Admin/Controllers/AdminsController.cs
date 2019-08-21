using HotelManagemant;
using HotelManagemant.ViewModels;
using PersonalBlog.Areas.Admin.Models;
using PersonalBlog.Areas.Data;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace PersonalBlog.Areas.Admin.Controllers
{

    public class AdminsController : Controller
    {
        private readonly PersonalDbContext context = new PersonalDbContext();
        private IAuthRepository auth;
        public AdminsController()
        {
            this.auth = new AuthRepository(context);
        }
        // GET: Admin/Admins
   
       
      
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        Role role = new Role();
        [HttpPost]
        public ActionResult Login(LoginViewModel l, string ReturnUrl)
        {

            ViewBag.ReturnUrl = ReturnUrl;


            if (auth.IsUserExists(l.Email))
            {
                var login = auth.Login(l.Email, l.Password);

                string pass = context.login.FirstOrDefault(a => (a.Email == l.Email)).RandomPass;


                if (login != null)
                {
                    var Admin = context.login.FirstOrDefault(a => (a.Email == l.Email));

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        var objAdmin = context.login.FirstOrDefault(a => (a.Email == l.Email));

                        FormsAuthentication.SetAuthCookie(l.Email, false);
                        Session.Add("id", Admin.Id);
                        Session.Add("userEmail", Admin.Email);
                        Session.Add("category", Admin.Role);

                        return Redirect(ReturnUrl);

                    }
                    else
                    {
                        Session.Add("id", Admin.Id);
                        Session.Add("userEmail", Admin.Email);
                        Session.Add("category", Admin.Role);
                        var objAdmin = context.login.FirstOrDefault(a => (a.Email == l.Email));
                        FormsAuthentication.SetAuthCookie(l.Email, false);
                        string[] roles = role.GetRolesForUser(objAdmin.Email);
                        if (roles.Contains("SuperAdmin"))
                        {
                            return RedirectToAction("Index", "Admin");

                        }
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "News");
                        }
                    
                    }

                }
                else if (l.Password == pass)
                {
                    TempData["message"] = l.Email;
                    return RedirectToAction("NewPassword");
                }


                else
                {
                    ModelState.AddModelError("", "Invalid Password");
                }

            }
            ModelState.AddModelError("", "Invalid User");

            return View();


        }

        public ActionResult NewPassword()
        {

            return PartialView();

        }
        [HttpPost]
        public ActionResult NewPassword(PasswordConform pass)
        {
            if (ModelState.IsValid)
            {
                string message = TempData["message"].ToString();
                var query = (from q in context.login
                             where q.Email == message
                             select q).First();
                string password = pass.Password;
                Login login = auth.registers(query, password);
                //query.RandomPass = null;

                return RedirectToAction("Login");



            }
            return PartialView();
        }
        public ActionResult Logout()
        {


            FormsAuthentication.SignOut();

            Session.Abandon();
            return RedirectToAction("Login");


        }

    }
}
