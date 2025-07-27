using Smockerie.Enum;

namespace Smockerie.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public OrderStatus Status { get; set; } = OrderStatus.Pending; 
        public DateTime CreatedAt { get; set; }
    }
}
