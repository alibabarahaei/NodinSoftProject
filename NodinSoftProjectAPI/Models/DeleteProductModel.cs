namespace NodinSoftProjectAPI.Models
{
    public class DeleteProductModel
    {
        public long ProductId { get; set; }
        public string EmailUser { get; set; }
        public bool IsDeletePermission { get; set; }

    }
}
