using BoutiqueApi.Models;
namespace BoutiqueApi.Models
{
    public class OrderProduct
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Order Order { get; set; } = null!;

      
    }
}