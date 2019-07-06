using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PagedList;

namespace ASP.NET_StoreManagement.Controllers
{
    public class ProductController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        public ActionResult ProductFirstStylePartial()
        {

            return PartialView();
        }

        public ActionResult ProductSecondStylePartial()
        {

            return PartialView();
        }

        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pr = db.Products.SingleOrDefault(p => p.Id == id && p.IsDeleted == false);
            if (pr == null)
            {
                return HttpNotFound();
            }
            return View(pr);
        }

        public ActionResult ProductListView(int? CategoryId, int? ManufacturerId, int? page )
        {
            if (CategoryId == null || ManufacturerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var list = db.Products.Where(p => p.CategoryId == CategoryId && p.ManufacturerId == ManufacturerId);
            if (list.Count() == 0)
            {
                return HttpNotFound();
            }

            int PageNumber = (page ?? 1); // Current Page Number
            int PageSize = 12; // Size of products in Page

            ViewBag.CategoryId = CategoryId;
            ViewBag.ManufacturerId = ManufacturerId;

            return View(list.OrderBy(p => p.Id).ToPagedList(PageNumber, PageSize));
        }
    }
}