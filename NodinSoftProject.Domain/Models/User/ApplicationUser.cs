using Microsoft.AspNetCore.Identity;
using NodinSoftProject.Domain.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace NodinSoftProject.Domain.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }


        [Display(Name = "تاریخ عضویت")] public DateTime CreationDate { get; set; } = DateTime.Now;


        #region relations


        public ICollection<ProductUser.ProductUser> UserProducts { get; set; }

        #endregion


    }
}
