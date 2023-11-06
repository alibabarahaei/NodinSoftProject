using NodinSoftProject.Domain.Models.User;
using System.ComponentModel.DataAnnotations;
using NodinSoftProject.Domain.Models.Base;

namespace NodinSoftProject.Domain.Models.Products
{
    public class Product:BaseEntity
    {


        [Display(Name = " نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = " ایمیل سازنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ManufactureEmail { get; set; }


        [Display(Name = " شماره تلفن سازنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(35, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ManufacturePhone { get; set; }


        public DateTime ProduceDate { get; set; } = DateTime.Now;


        public bool IsAvailable { get; set; } = true;

        #region relations

        public ApplicationUser User { get; set; }

        #endregion


    }
}
