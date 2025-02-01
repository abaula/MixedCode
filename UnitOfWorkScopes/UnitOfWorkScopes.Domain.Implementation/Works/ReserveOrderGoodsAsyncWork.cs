using System;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Commands;
using UnitOfWorkScopes.Domain.Abstractions.Works;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class ReserveOrderGoodsAsyncWork : IReserveOrderGoodsAsyncWork
    {
        private readonly IUnitOfWorkScopeProxy _scope;

        public ReserveOrderGoodsAsyncWork(IUnitOfWorkScopeProxy scope)
        {
            _scope = scope;
        }

        public async Task DoAsync(Guid data)
        {
            // Получаем список товаров и ставим их в резерв.
            var goods = await _scope.Get<IGetOrderGoodsAsyncWork>()
                       .DoAsync(data)
                       .ConfigureAwait(false);

            if (!goods.Any())
                return;

            await _scope.Get<IReserveGoodsCmd>()
                .ExecuteAsync(goods.Select(g => g.Id))
                .ConfigureAwait(false);
        }
    }
}
