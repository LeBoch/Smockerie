
namespace BoutiqueApi.Models
{
    public class Flavor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}