using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_StoreManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Products.OrderByDescending(d => d.Id));
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(d => d.Id), "Id", "DisplayName");
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(d => d.Id), "Id", "DisplayName");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(d => d.Id), "Id", "DisplayName");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase[] ImageFile)
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(d => d.Id), "Id", "DisplayName");
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(d => d.Id), "Id", "DisplayName");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(d => d.Id), "Id", "DisplayName");
            int error = 0;

            for (int i = 0; i < ImageFile.Count(); i++)
            {
                if (ImageFile[i] != null)
                {
                    if (ImageFile[i].ContentLength > 0)
                    {
                        if (ImageFile[i].ContentType != "image/jpeg" && ImageFile[i].ContentType != "image/png" && ImageFile[i].ContentType != "image/gif" && ImageFile[i].ContentType != "image/jpg")
                        {
                            ViewBag.FirstMess += "Hình ảnh" + i + " không hợp lệ <br />";
                            error++;
                        }
                        else
                        {
                            var fileName = Path.GetFileName(ImageFile[i].FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.SecondMess += "Hình ảnh " + i + " đã tồn tại <br />";
                                error++;
                            }
                            else
                            {
                                ImageFile[i].SaveAs(path);
                                ViewBag.SecondMess = "";
                            }

                            if (i == 0)
                            {
                                product.Image = ImageFile[0].FileName;
                            }
                            else if (i == 1)
                            {
                                product.ChildImage1 = ImageFile[1].FileName;
                            }
                            else if (i == 2)
                            {
                                product.ChildImage2 = ImageFile[2].FileName;
                            }
                            else if (i == 3)
                            {
                                product.ChildImage3 = ImageFile[3].FileName;
                            }
                            else if (i == 4)
                            {
                                product.ChildImage4 = ImageFile[4].FileName;
                            }
                        }
                        
                    }
                }
            }
            
            if (error > 0)
            {
                return View(product);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index", "ProductManager");
        }

        [HttpGet]
        public ActionResult UpdateProduct(int? Id)
        {
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
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(d => d.Id), "Id", "DisplayName", product.SupplierId);
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(d => d.Id), "Id", "DisplayName", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(d => d.Id), "Id", "DisplayName", product.ManufacturerId);

            return View(product);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateProduct(Product product, HttpPostedFileBase[] ImageFile)
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(d => d.Id), "Id", "DisplayName", product.SupplierId);
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(d => d.Id), "Id", "DisplayName", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(d => d.Id), "Id", "DisplayName", product.ManufacturerId);
            int error = 0;

            for (int i = 0; i < ImageFile.Count(); i++)
            {
                if (ImageFile[i] != null)
                {
                    if (ImageFile[i].ContentLength > 0)
                    {
                        if (ImageFile[i].ContentType != "image/jpeg" && ImageFile[i].ContentType != "image/png" && ImageFile[i].ContentType != "image/gif" && ImageFile[i].ContentType != "image/jpg")
                        {
                            ViewBag.FirstMess += "Hình ảnh" + i + " không hợp lệ <br />";
                            error++;
                        }
                        else
                        {
                            var fileName = Path.GetFileName(ImageFile[i].FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.SecondMess += "Hình ảnh " + i + " đã tồn tại <br />";
                                error++;
                            }
                            else
                            {
                                ImageFile[i].SaveAs(path);
                                ViewBag.SecondMess = "";
                            }

                            if (i == 0)
                            {
                                product.Image = ImageFile[0].FileName;
                            }
                            else if (i == 1)
                            {
                                product.ChildImage1 = ImageFile[1].FileName;
                            }
                            else if (i == 2)
                            {
                                product.ChildImage2 = ImageFile[2].FileName;
                            }
                            else if (i == 3)
                            {
                                product.ChildImage3 = ImageFile[3].FileName;
                            }
                            else if (i == 4)
                            {
                                product.ChildImage4 = ImageFile[4].FileName;
                            }
                        }

                    }
                }
            }

            if (error > 0)
            {
                return View(product);
            }

            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int? Id)
        {
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
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(d => d.Id), "Id", "DisplayName", product.SupplierId);
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(d => d.Id), "Id", "DisplayName", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(d => d.Id), "Id", "DisplayName", product.ManufacturerId);

            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int Id)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}