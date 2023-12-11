using Microsoft.AspNetCore.SignalR;
using NodinSoftProject.Application.DTOs.Account;

namespace NodinSoftProjectAPI.Models
{
    public class AddDeletePermissionModel
    {
        public  string UserEmail { get; set; }
        public long ProductId { get; set; }
        public int IsDeletePermission { get; set; }
    }
}
