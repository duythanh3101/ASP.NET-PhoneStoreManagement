using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ASP.NET_StoreManagement.Controllers
{
    public class SearchController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        // GET: Search
        [HttpGet]
        public ActionResult Search(string searchText, int page = 1, int pageSize = 12)
        {
            int PageNumber = page; // Current Page Number
   
            var lstProducts = db.Products.Where(p => p.DisplayName.Contains(searchText));
            ViewBag.SearchText = searchText;

            return View(lstProducts.OrderBy(d=>d.DisplayName).ToPagedList(PageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult SearchText(string searchText)
        {
            return RedirectToAction("Search", new { @searchText = searchText });
        }

        public ActionResult SearchTextByAjax(string searchText, int page = 1, int pageSize = 12)
        {
            int PageNumber = page; // Current Page Number

            var lstProducts = db.Products.Where(p => p.DisplayName.Contains(searchText));
            ViewBag.SearchText = searchText;

            return PartialView(lstProducts.OrderBy(d=>d.UnitPrice));
        }
    }
}