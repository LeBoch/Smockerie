namespace Smockerie.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using BoutiqueApi.Data;
    using BoutiqueApi.Models;
    using Smockerie.DTO;

        public class ProductService : IProductService
        {
            private readonly BoutiqueContext _ctx;
            public ProductService(BoutiqueContext ctx) => _ctx = ctx;

            public async Task<IEnumerable<Product>> GetAllAsync() =>
                await _ctx.Products.ToListAsync();

            public async Task<Product?> GetByIdAsync(int id) =>
                await _ctx.Products.FindAsync(id);

        public async Task<Product> CreateAsync(ProductCreateDto input)
        {
            var product = new Product
            {
                Name = input.Name,
                Price = input.Price,
                Stock = input.Stock,
                ImageUrl = input.ImageUrl,
                CategoryId = input.CategoryId,
                FlavorId = input.FlavorId
            };

            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product p)
            {
                _ctx.Entry(p).State = EntityState.Modified;
                try
                {
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return !await _ctx.Products.AnyAsync(x => x.Id == p.Id);
                }
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                var p = await _ctx.Products.FindAsync(id);
                if (p == null) return false;
                _ctx.Products.Remove(p);
                await _ctx.SaveChangesAsync();
                return true;
            }
        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId)
        {
            return await _ctx.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    imageUrl = p.ImageUrl,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();
        }
    }
    }
