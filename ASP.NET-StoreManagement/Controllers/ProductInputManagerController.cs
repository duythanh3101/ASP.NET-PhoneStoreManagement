using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_StoreManagement.Controllers
{
    public class ProductInputManagerController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        [HttpGet]
        public ActionResult Input()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "DisplayName");
            ViewBag.ListProducts = new SelectList(db.Products, "Id", "DisplayName");
            return View();
        }

        [HttpPost]
        public ActionResult Input(Input input, IEnumerable<InputDetail> list)
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "DisplayName");
            ViewBag.ListProducts = new SelectList(db.Products, "Id", "DisplayName");

            //Lưu phiếu nhập
            input.IsDeleted = false;
            db.Inputs.Add(input);
            db.SaveChanges();

            //Lưu chi tiếp phiếu nhập
            foreach (var item in list)
            {
                item.InputId = input.Id;
                db.InputDetails.Add(item);
                //Thêm số lượng vào bảng sản phẩm
                Product product = db.Products.SingleOrDefault(n => n.Id == item.ProductId);
                if (product != null)
                {
                    product.InventoryCount += item.Amount;
                }
            }
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult ListProductsOutOfStock()
        {
            return View(db.Products.Where(d => d.InventoryCount <= 5 && d.IsDeleted == false));
        }

        [HttpGet]
        public ActionResult InputSingleProduct(int? Id)
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "DisplayName");

            if (Id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Product product = db.Products.SingleOrDefault(n => n.Id == Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult InputSingleProduct(Input input, InputDetail detail)
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "DisplayName", input.SupplierId);

            //Lưu phiếu nhập
            input.IsDeleted = false;
            input.InputDate = DateTime.Now;
            db.Inputs.Add(input);
            db.SaveChanges();

            //Lưu chi tiếp phiếu nhập

            detail.InputId = input.Id;
            db.InputDetails.Add(detail);
            //Thêm số lượng vào bảng sản phẩm
            Product product = db.Products.SingleOrDefault(n => n.Id == detail.ProductId);
            if (product != null)
            {
                product.InventoryCount += detail.Amount;
            }

            db.SaveChanges();
            return View(product);
        }

        //Giải phóng biến cho vùng nhớ
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}