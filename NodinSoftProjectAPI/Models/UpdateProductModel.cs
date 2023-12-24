using System.ComponentModel.DataAnnotations;

namespace NodinSoftProjectAPI.Models
{
    public class UpdateProductModel
    {

       
        public long ProductId { get; set; }

      
        public string ProductName { get; set; }

       
        [EmailAddress]
        public string ManufactureEmail { get; set; }

       
        public string ManufacturePhone { get; set; }


    }
}
