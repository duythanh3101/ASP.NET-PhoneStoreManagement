using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_StoreManagement.Controllers
{
    public class AdminController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Products);
        }
    }
}