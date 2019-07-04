//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP.NET_StoreManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.InputDetails = new HashSet<InputDetail>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Configuration { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<int> InventoryCount { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public Nullable<int> VoteCount { get; set; }
        public Nullable<int> CommentCount { get; set; }
        public Nullable<int> PurchaseCount { get; set; }
        public Nullable<int> IsNewProduct { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string ChildImage1 { get; set; }
        public string ChildImage2 { get; set; }
        public string ChildImage3 { get; set; }
        public string ChildImage4 { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputDetail> InputDetails { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}