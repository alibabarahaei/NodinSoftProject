using System.ComponentModel.DataAnnotations;

namespace NodinSoftProjectAPI.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور یکسان نیست")]
        public string ConfirmPassword { get; set; }
    }
}
