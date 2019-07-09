using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_StoreManagement.Models
{
    [MetadataTypeAttribute(typeof(ProductMetadata))]
    public partial class Product
    {
        internal sealed class ProductMetadata
        {
            public int Id { get; set; }
            [DisplayName("Tên sản phẩm")]
            public string DisplayName { get; set; }
            [DisplayName("Giá")]
            public Nullable<decimal> UnitPrice { get; set; }
            [DisplayName("Ngày cập nhật")]
            public Nullable<System.DateTime> UpdateDate { get; set; }
            [DisplayName("Cấu hình")]
            public string Configuration { get; set; }
            [DisplayName("Mô tả")]
            public string Description { get; set; }
            [DisplayName("Hình ảnh")]
            public string Image { get; set; }
            [DisplayName("Số lượng")]
            public Nullable<int> InventoryCount { get; set; }
            [DisplayName("Lượt xem")]
            public Nullable<int> ViewCount { get; set; }
            [DisplayName("Lượt chọn")]
            public Nullable<int> VoteCount { get; set; }
            [DisplayName("Lượt bình luận")]
            public Nullable<int> CommentCount { get; set; }
            [DisplayName("Lượt mua")]
            public Nullable<int> PurchaseCount { get; set; }
            [DisplayName("Sản phẩm mới")]
            public Nullable<int> IsNewProduct { get; set; }
            [DisplayName("Mã nhà cung cấp")]
            public Nullable<int> SupplierId { get; set; }
            [DisplayName("Mã nhà sản xuất")]
            public Nullable<int> ManufacturerId { get; set; }
            [DisplayName("Mã loại sản phẩm")]
            public Nullable<int> CategoryId { get; set; }
            [DisplayName("Đã xóa")]
            public Nullable<bool> IsDeleted { get; set; }
            [DisplayName("Hình ảnh 1")]
            public string ChildImage1 { get; set; }
            [DisplayName("Hình ảnh 2")]
            public string ChildImage2 { get; set; }
            [DisplayName("Hình ảnh 3")]
            public string ChildImage3 { get; set; }
            [DisplayName("Hình ảnh 4")]
            public string ChildImage4 { get; set; }

        }
    }
}