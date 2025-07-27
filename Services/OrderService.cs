// Smockerie/Services/OrderService.cs
using Microsoft.EntityFrameworkCore;
using BoutiqueApi.Data;
using BoutiqueApi.Models;

namespace Smockerie.Services
{
    public class OrderService : IOrderService
    {
        private readonly BoutiqueContext _ctx;
        public OrderService(BoutiqueContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _ctx.Orders.ToListAsync();

        public async Task<Order?> GetByIdAsync(Guid id) =>
            await _ctx.Orders.FindAsync(id);

        //public async Task<Order> CreateAsync(Order order)
        //{
        //    _ctx.Orders.Add(order);
        //    await _ctx.SaveChangesAsync();
        //    return order;
        //}

        //public async Task<bool> UpdateAsync(Order order)
        //{
        //    _ctx.Entry(order).State = EntityState.Modified;
        //    try
        //    {
        //        await _ctx.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return !await _ctx.Orders.AnyAsync(o => o.Id == order.Id);
        //    }
        //}

        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var order = await _ctx.Orders.FindAsync(id);
        //    if (order == null) return false;
        //    _ctx.Orders.Remove(order);
        //    await _ctx.SaveChangesAsync();
        //    return true;
        //}
    }
}