using System;
using System.Threading.Tasks;

namespace UnitOfWorkScopes.Services.Abstractions
{
    public interface IOrdersService
    {
        Task<decimal> GetOrderAmountAsync(Guid orderId);
        Task ApproveOrderAsync(Guid orderId);
    }
}
