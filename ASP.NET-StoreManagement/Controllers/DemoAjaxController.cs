using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_StoreManagement.Controllers
{
    public class DemoAjaxController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        // GET: DemoAjax
        public ActionResult DemoAjax()
        {
            return View();
        }

        public ActionResult LoadAjaxActionLink()
        {
            System.Threading.Thread.Sleep(3000);
            return Content("hello");
        }

        public ActionResult ProductPartial()
        {
            var lst = db.Products;
          

            return PartialView("ProductPartial", lst);
        }
    }
}