// Smockerie/Services/IOrderProductService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueApi.Models;

namespace Smockerie.Services
{
    public interface IOrderProductService
    {
        Task<IEnumerable<OrderProduct>> GetAllAsync();
        Task<OrderProduct?> GetByIdAsync(Guid id);
        //Task<bool> UpdateAsync(OrderProduct orderProduct);
        //Task<bool> DeleteAsync(Guid id);
    }
}
