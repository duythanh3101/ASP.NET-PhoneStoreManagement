using ASP.NET_StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_StoreManagement.Controllers
{
    public class OrderManagerController : Controller
    {
        DBStoreManagmentEntities db = new DBStoreManagmentEntities();

        // GET: OrderManager
        public ActionResult Unpaid()
        {
            var lst = db.Orders.Where(d => d.IsPaid == false).OrderBy(n=>n.OrderDate);
            return View(lst);
        }

        public ActionResult UnDelivery()
        {
            var lst = db.Orders.Where(d => d.OrderState == false).OrderBy(n => n.OrderDate);
            return View(lst);
        }

        public ActionResult PaidAndDelivery()
        {
            var lst = db.Orders.Where(d => d.OrderState == true && d.IsPaid == true).OrderBy(n => n.OrderDate);
            return View(lst);
        }

        [HttpGet]
        public ActionResult ReviewOrder(int? Id)
        {
            if (Id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Order order = db.Orders.SingleOrDefault(d => d.Id == Id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.listOrderDeitals = db.OrderDetails.Where(n => n.Id == Id);          
            return View(order);
        }

        [HttpPost]
        public ActionResult ReviewOrder(Order order)
        {
            Order or = db.Orders.Single(o => o.Id == order.Id);
            or.IsPaid = order.IsPaid;
            or.OrderState = order.OrderState;
            db.SaveChanges();

            ViewBag.listOrderDeitals = db.OrderDetails.Where(n => n.Id == order.Id);
            if (!string.IsNullOrEmpty(or.Customer.Email))
            {
                SendEmail("Xác nhận đơn hàng", or.Customer.Email, "lamnguyenngoccamtien123@gmail.com", "fgakdhsj", string.Format("xác nhận đơn hàng có mã đơn hàng: " + or.Id));
            }
            return View(or);
        }

        public void SendEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); 
            mail.Subject = Title; 
            mail.Body = Content;               
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;              
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);
            smtp.EnableSsl = true;   //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail);   //Gửi mail đi
        }


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