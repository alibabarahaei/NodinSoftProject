using Microsoft.AspNetCore.SignalR;
using NodinSoftProject.Application.DTOs.Account;
using System.ComponentModel.DataAnnotations;

namespace NodinSoftProjectAPI.Models
{
    public class AddDeletePermissionModel
    {
        [Required(ErrorMessage = "لطفا ایمیل را وارد کنید")]
        [EmailAddress]
        public  string UserEmail { get; set; }
        [Required(ErrorMessage = "لطفا شماره محصول را وارد کنید")]
        public long ProductId { get; set; }
        [Required(ErrorMessage = "لطفا مجوز پاک کردن را وارد کنید")]
        public bool IsDeletePermission { get; set; }
    }
}
