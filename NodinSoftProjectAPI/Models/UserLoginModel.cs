using System.ComponentModel.DataAnnotations;

namespace NodinSoftProjectAPI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
