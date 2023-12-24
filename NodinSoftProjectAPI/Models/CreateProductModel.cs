using System.ComponentModel.DataAnnotations;

namespace NodinSoftProjectAPI.Models
{
    public class CreateProductModel
    {

        [Required(ErrorMessage = "لطفا نام محصول  را وارد کنید")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        public string ManufactureEmail { get; set; }
        [Required(ErrorMessage = "لطفا شماره تلفن  سازنده را وارد کنید")]
        public string ManufacturePhone { get; set; }

    }
}
