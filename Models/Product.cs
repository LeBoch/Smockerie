namespace BoutiqueApi.Models
{
    public class Product
    {
        public int Id { get; set; }         // Clé primaire
        public string Name { get; set; }    // Nom du produit
        public decimal Price { get; set; }  // Prix
        public int Stock { get; set; }      // Quantité en stock
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int FlavorId { get; set; }
        public Flavor Flavor { get; set; }

    }
}