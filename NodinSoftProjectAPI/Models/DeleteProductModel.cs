using System.ComponentModel.DataAnnotations;

namespace NodinSoftProjectAPI.Models
{
    public class DeleteProductModel
    {
        [Required(ErrorMessage = "لطفا شماره محصول را وارد کنید")]
        public long ProductId { get; set; }
        [Required(ErrorMessage = "لطفا مجوز پاک شدن  را وارد کنید")]
        public bool IsDeletePermission { get; set; }

    }
}
