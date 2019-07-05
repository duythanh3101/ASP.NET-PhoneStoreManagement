using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_StoreManagement.Controllers
{
    public class BasketController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        public List<BasketDetail> GetBasket()
        {
            List <BasketDetail> basket = Session["Basket"] as List<BasketDetail>;
            if (basket == null)
            {
                basket = new List<BasketDetail>();
                Session["Basket"] = basket;
            }

            return basket;
        }

        public int GetAmountOfProducts()
        {
            List<BasketDetail> basket = GetBasket();
            if (basket.Count == 0)
            {
                return 0;
            }
            return basket.Sum(p => p.Amount);
        }

        public decimal GetTotalPrice()
        {
            List<BasketDetail> basket = Session["Basket"] as List<BasketDetail>;
            if (basket.Count == 0)
            {
                return 0;
            }
            return basket.Sum(p => p.Total);
        }

        public ActionResult AddProduct(int Id, string directURL)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == Id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<BasketDetail> basket = GetBasket();
            BasketDetail existingDetail = basket.SingleOrDefault(p => p.Id == Id);
            if (existingDetail != null)
            {
                if (existingDetail.Amount < product.InventoryCount)
                {
                    existingDetail.Amount++;
                    existingDetail.Total = existingDetail.Amount * existingDetail.UnitPrice;
                    return Redirect(directURL);
                }
                return View("Notify");
            }
            BasketDetail basketDetail = new BasketDetail(Id);
            basket.Add(basketDetail);

            return Redirect(directURL);
        }
        public ActionResult AddProductAjax(int Id, string directURL)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == Id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<BasketDetail> basket = GetBasket();
            BasketDetail existingDetail = basket.SingleOrDefault(p => p.Id == Id);
            if (existingDetail != null)
            {
                if (existingDetail.Amount < product.InventoryCount)
                {
                    existingDetail.Amount++;
                    existingDetail.Total = existingDetail.Amount * existingDetail.UnitPrice;
                    ViewBag.TotalPrice = GetTotalPrice();
                    ViewBag.Amount = GetAmountOfProducts();
                    return PartialView("BasketPartial");
                }
                return Content("<script type=\"text/javascript\"> alert(\"Sản phẩm đã hết hàng!\")</script>");
            }
            BasketDetail basketDetail = new BasketDetail(Id);
            basket.Add(basketDetail);

            ViewBag.TotalPrice = GetTotalPrice();
            ViewBag.Amount = GetAmountOfProducts();
            return PartialView("BasketPartial");
        }

        // GET: Basket
        public ActionResult BasketView()
        {
            List<BasketDetail> basket = GetBasket();
            return View(basket);
        }

        public ActionResult BasketPartial()
        {
            int amount = GetAmountOfProducts();
            if (amount == 0)
            {
                ViewBag.Amount = 0;
                ViewBag.TotalPrice = 0;
            }
            else
            {
                ViewBag.Amount = amount;
                ViewBag.TotalPrice = GetTotalPrice();
            }
            return PartialView();
        }

        [HttpGet]
        public ActionResult UpdateProduct(int Id)
        {
            if (Session["Basket"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product product = db.Products.SingleOrDefault(p => p.Id == Id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<BasketDetail> basket = GetBasket();
            BasketDetail existingDetail = basket.SingleOrDefault(p => p.Id == Id);
            if (existingDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Basket = basket;
            return View(existingDetail);
        }

        [HttpPost]
        public ActionResult UpdateProduct(BasketDetail detail)
        {
            Product product = db.Products.Single(p => p.Id == detail.Id);
            if (detail.Amount > product.InventoryCount)
            {
                return View("Notify");
            }
            List<BasketDetail> basket = GetBasket();
            BasketDetail updatedDetail = basket.Single(p => p.Id == detail.Id);
            updatedDetail.Amount = detail.Amount;
            updatedDetail.Total = updatedDetail.Amount * updatedDetail.UnitPrice;

            return RedirectToAction("BasketView");
        }

        public ActionResult DeleteProduct(int Id)
        {
            if (Session["Basket"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product product = db.Products.SingleOrDefault(p => p.Id == Id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<BasketDetail> basket = GetBasket();
            BasketDetail existingDetail = basket.SingleOrDefault(p => p.Id == Id);
            if (existingDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            basket.Remove(existingDetail);
            return RedirectToAction("BasketView");
        }

        public ActionResult Order(Customer customer)
        {
            if (Session["Basket"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lấy thông tin khách hàng
            Customer cus = new Customer();
            //TH1:Chưa đăng nhập
            if (Session["Account"] == null)
            {
                cus = customer;
            }
            //TH2: Đã đăng nhập
            else
            {
                User user = Session["Account"] as User;
                cus.UserId = user.Id;
                cus.DisplayName = user.FullName;
                cus.Email = user.Email;
                cus.Address = user.Address;
                cus.PhoneNumber = user.PhoneNumber;
            }
            db.Customers.Add(cus);
            db.SaveChanges();

            //Add order
            Order order = new Order();
            order.CustomerId = cus.Id;
            order.OrderDate = DateTime.Now;
            order.OrderState = false;
            order.Promotion = 0;
            order.IsPaid = false;
            order.IsDeleted = false;
            order.IsCancelled = false;
            db.Orders.Add(order);
            db.SaveChanges();
            //Add order details
            List<BasketDetail> basket = GetBasket();
            foreach (var item in basket)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderId = order.Id;
                detail.ProductId = item.Id;
                detail.Quantity = item.Amount;
                detail.Price = item.UnitPrice;
                detail.DisplayName = item.DisplayName;
                db.OrderDetails.Add(detail);
            }
            db.SaveChanges();
            Session["Basket"] = null;
            return Redirect("BasketView");
        }
    }
}