
using NodinSoftProject.Domain.Models.Base;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.User;

namespace NodinSoftProject.Domain.Models.ProductUser
{
    public class ProductUser: BaseEntity
    {
        
        public long ProductId { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public bool EditAccess { get; set; }
        public bool DeleteAccess { get; set; }
    }
}
