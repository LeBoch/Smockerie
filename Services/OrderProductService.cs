// Smockerie/Services/OrderProductService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoutiqueApi.Data;
using BoutiqueApi.Models;

namespace Smockerie.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly BoutiqueContext _ctx;
        public OrderProductService(BoutiqueContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<OrderProduct>> GetAllAsync() =>
            await _ctx.OrderProducts.ToListAsync();

        public async Task<OrderProduct?> GetByIdAsync(Guid id) =>
            await _ctx.OrderProducts.FindAsync(id);


        //public async Task<bool> UpdateAsync(OrderProduct op)
        //{
        //    _ctx.Entry(op).State = EntityState.Modified;
        //    try
        //    {
        //        await _ctx.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return !await _ctx.OrderProducts.AnyAsync(e => e.Id == op.Id);
        //    }
        //}

        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var op = await _ctx.OrderProducts.FindAsync(id);
        //    if (op == null) return false;
        //    _ctx.OrderProducts.Remove(op);
        //    await _ctx.SaveChangesAsync();
        //    return true;
        //}
    }
}
