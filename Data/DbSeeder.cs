using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using BoutiqueApi.Models;
using BoutiqueApi.Data;
using Smockerie.Enum;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueApi
{
    public static class DbInitializer
    {
        public static void Initialize(BoutiqueContext context)
        {
            // 1) S'assure que la BDD et les tables sont créées
            context.Database.Migrate();

            // 2) Si des catégories sont déjà présentes, on sort
            if (context.Categories.Any())
                return;

            // 3) Seed des catégories
            var categories = new[]
            {
                new Category { Id = 1, Name = "Ice Cream" },
                new Category { Id = 2, Name = "Donut" }
            };
            context.Categories.AddRange(categories);

            // 4) Seed des flavors
            var flavors = new[]
            {
                new Flavor { Id = 1, Name = "Vanilla" },
                new Flavor { Id = 2, Name = "Chocolate" }
            };
            context.Flavors.AddRange(flavors);

            // 5) Seed des produits
            var products = new[]
            {
                new Product { Id =  1, Name = "Vanilla Cone",           Price = 2.50m, Stock = 100, ImageUrl = "/images/vanilla-cone.jpg",           CategoryId = 1, FlavorId = 1 },
                new Product { Id =  2, Name = "Vanilla Cup",            Price = 3.00m, Stock = 100, ImageUrl = "/images/vanilla-cup.jpg",            CategoryId = 1, FlavorId = 1 },
                new Product { Id =  3, Name = "Vanilla Sundae",         Price = 4.50m, Stock =  80, ImageUrl = "/images/vanilla-sundae.jpg",         CategoryId = 1, FlavorId = 1 },
                new Product { Id =  4, Name = "Chocolate Cone",         Price = 2.75m, Stock = 100, ImageUrl = "/images/chocolate-cone.jpg",         CategoryId = 1, FlavorId = 2 },
                new Product { Id =  5, Name = "Chocolate Cup",          Price = 3.25m, Stock = 100, ImageUrl = "/images/chocolate-cup.jpg",          CategoryId = 1, FlavorId = 2 },
                new Product { Id =  6, Name = "Vanilla Donut",          Price = 1.50m, Stock = 150, ImageUrl = "/images/vanilla-donut.jpg",          CategoryId = 2, FlavorId = 1 },
                new Product { Id =  7, Name = "Chocolate Donut",        Price = 1.75m, Stock = 150, ImageUrl = "/images/chocolate-donut.jpg",        CategoryId = 2, FlavorId = 2 },
                new Product { Id =  8, Name = "Vanilla Filled Donut",   Price = 2.00m, Stock = 120, ImageUrl = "/images/vanilla-filled-donut.jpg",   CategoryId = 2, FlavorId = 1 },
                new Product { Id =  9, Name = "Chocolate Filled Donut", Price = 2.25m, Stock = 120, ImageUrl = "/images/chocolate-filled-donut.jpg", CategoryId = 2, FlavorId = 2 },
                new Product { Id = 10, Name = "Vanilla Sprinkles Donut",Price = 2.50m, Stock = 100, ImageUrl = "/images/vanilla-sprinkles.jpg",        CategoryId = 2, FlavorId = 1 },
            };
            context.Products.AddRange(products);

            // 6) Seed de l'utilisateur test
            var testUser = new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Username = "testuser",
                Email = "test@example.com",
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            // Hash du mot de passe “Test@123” (vous pouvez changer la valeur claire)
            var hasher = new PasswordHasher<User>();
            testUser.PasswordHash = hasher.HashPassword(testUser, "Test@123");
            context.Users.Add(testUser);

            // 7) Enregistre tout en base
            context.SaveChanges();
        }
    }
}
