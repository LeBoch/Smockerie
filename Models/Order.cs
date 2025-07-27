using Smockerie.Enum;

namespace BoutiqueApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}