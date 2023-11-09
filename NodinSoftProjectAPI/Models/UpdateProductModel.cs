namespace NodinSoftProjectAPI.Models
{
    public class UpdateProductModel
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public string ManufactureEmail { get; set; }

        public string ManufacturePhone { get; set; }

        public bool IsAvailable { get; set; }
    }
}
