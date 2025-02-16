using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Elysian.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "The StoreID field is required.")]
        public int StoreID { get; set; } // Foreign key

        [Required(ErrorMessage = "The ProductName field is required.")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Stock field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative number.")]
        public int Stock { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        // Navigation property for EF Core (excluded from validation/serialization)
        [JsonIgnore]
        public Store? Store { get; set; }
    }
}
