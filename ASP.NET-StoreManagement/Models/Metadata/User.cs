using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_StoreManagement.Models
{
    [MetadataTypeAttribute(typeof(UserMetadata))]
    public partial class User
    {
        internal sealed class UserMetadata
        {
            public int Id { get; set; }
            [DisplayName("Tài khoản")]
            [StringLength(12, MinimumLength = 5, ErrorMessage ="{0} quá ngắn hoặc quá dài")]
            [Required(ErrorMessage ="{0} không hợp lệ")]
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Question { get; set; }
            public string Answer { get; set; }
            public Nullable<int> UserTypeId { get; set; }
        }
    }
}