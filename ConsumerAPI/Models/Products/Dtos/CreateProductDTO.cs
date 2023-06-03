using System.ComponentModel.DataAnnotations;

namespace ConsumerAPI.Models.Products.Dtos
{
    public class CreateProductDTO
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public long SupplierId { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public string QuantityPerUnit { get; set; }
        [Required]
        public byte[] UnitPrice { get; set; } = null!;
        [Required]
        public long UnitsInStock { get; set; }
        [Required]
        public long UnitsOnOrder { get; set; }
        [Required]
        public long ReorderLevel { get; set; }
        [Required]
        public long Discontinued { get; set; }
    }
}
