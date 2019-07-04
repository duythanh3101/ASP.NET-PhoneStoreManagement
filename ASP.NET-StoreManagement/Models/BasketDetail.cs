using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_StoreManagement.Models
{
    public class BasketDetail
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string Image { get; set; }
     
        public BasketDetail(int id)
        {
            using (DBStoreManagmentEntities db = new DBStoreManagmentEntities())
            {
                Id = id;
                var product = db.Products.Single(p => p.Id == id);
                DisplayName = product.DisplayName;
                Amount = 1;
                UnitPrice = product.UnitPrice.Value;
                Total = UnitPrice * Amount;
                Image = product.Image;
            }
        }
        
        public BasketDetail()
        {

        }

    }
}