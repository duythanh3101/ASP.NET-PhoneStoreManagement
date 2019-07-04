using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;

namespace ASP.NET_StoreManagement.Controllers
{
    public class HomeController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        public ActionResult Index()
        {
            var listPhones = db.Products.Where(p => p.CategoryId == 1 && p.IsNewProduct == 1 && p.IsDeleted == false);
            var listLaptops = db.Products.Where(p => p.CategoryId == 3 && p.IsNewProduct == 1 && p.IsDeleted == false);
            var listTablets = db.Products.Where(p => p.CategoryId == 2 && p.IsNewProduct == 1 && p.IsDeleted == false);

            ViewBag.ListPhones = listPhones;
            ViewBag.ListLaptops = listLaptops;
            ViewBag.ListTablets = listTablets;
            return View();
        }

        public ActionResult MenuPartialView()
        {
            var list = db.Products;
            return PartialView(list);
        }

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.Notify = "Thêm thành công";
                db.Users.Add(user);
                db.SaveChanges();
                return View();
            }
            ViewBag.Notify = "Thêm thất bại";

            return View();
        }

        public ActionResult Login(FormCollection f)
        {
            string UserName = f["UserName"].ToString();
            string Password = f["Password"].ToString();

            User user = db.Users.SingleOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user != null)
            {
                Session["Account"] = user;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

   
        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Index");
        }
   }
}