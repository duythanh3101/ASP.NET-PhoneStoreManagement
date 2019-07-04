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
    }
}