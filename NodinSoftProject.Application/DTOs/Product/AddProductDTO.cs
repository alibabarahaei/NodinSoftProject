using Microsoft.AspNetCore.Http;

namespace NodinSoftProject.Application.DTOs.Product
{
    public class AddProductDTO
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string ManufactureEmail { get; set; }

        public string ManufacturePhone { get; set; }

    }
}
