using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Commands;
using UnitOfWorkScopes.Domain.Abstractions.Works;
using UnitOfWorkScopes.Services.Abstractions;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;

namespace UnitOfWorkScopes.Services.Implementation
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWorkScopeFactory _scopeFactory;
        private readonly ILogger _logger;

        public OrdersService(IUnitOfWorkScopeFactory scopeFactory, ILogger<OrdersService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task<decimal> GetOrderAmountAsync(Guid orderId)
        {
            using (var scope = _scopeFactory.Create())
            {
                _logger.LogTrace("Start => GetOrderAmountAsync({0})", orderId);

                var amount = await scope.Get<IGetOrderAmountAsyncWork>()
                    .DoAsync(orderId)
                    .ConfigureAwait(false);

                _logger.LogTrace("End => GetOrderAmountAsync({0}) = {1}", orderId, amount);

                return amount;
            }
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            using (var scope = _scopeFactory.Create(IsolationLevel.Serializable))
            {
                _logger.LogTrace("Start => DeleteOrderAsync({0})", orderId);


                scope.Commit();

                _logger.LogTrace("End => DeleteOrderAsync({0})", orderId);
            }
        }

        public async Task ApproveOrderAsync(Guid orderId)
        {
            using (var scope = _scopeFactory.Create(IsolationLevel.RepeatableRead))
            {
                _logger.LogTrace("Start => ApproveOrderAsync({0})", orderId);

                var amount = await scope.Get<IGetOrderAmountAsyncWork>()
                    .DoAsync(orderId)
                    .ConfigureAwait(false);

                if (amount > 0)
                {
                    // Ставим резерв на все товары заказа.
                    await scope.Get<IReserveOrderGoodsAsyncWork>()
                        .DoAsync(orderId)
                        .ConfigureAwait(false);
                }

                // Утверждаем заказ
                await scope.Get<IApproveOrderCmd>()
                    .ExecuteAsync(orderId)
                    .ConfigureAwait(false);

                scope.Commit();

                _logger.LogTrace("End => ApproveOrderAsync({0})", orderId);
            }
        }
    }
}
