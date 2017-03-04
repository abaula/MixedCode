using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Commands;
using UnitOfWorkScopes.Domain.Abstractions.Works;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class ReserveOrderGoodsWork : IWorkAsync<ReserveOrderGoodsWorkDto>
    {
        private readonly IUnitOfWorkScopeProxy _scope;

        public ReserveOrderGoodsWork(IUnitOfWorkScopeProxy scope)
        {
            _scope = scope;
        }

        public async Task DoAsync(ReserveOrderGoodsWorkDto data)
        {
            // Получаем список товаров и ставим их в резерв.
            var goods = await _scope.Get<IWorkAsync<GetOrderGoodsWorkDto, GoodsInfoDto[]>>()
                       .DoAsync(new GetOrderGoodsWorkDto { OrderId = data.OrderId })
                       .ConfigureAwait(false);

            if (!goods.Any())
                return;

            await _scope.Get<ICommandAsync<ReserveGoodsCmdDto>>()
                .ExecuteAsync(new ReserveGoodsCmdDto { GoodsIds = goods.Select(g => g.Id) })
                .ConfigureAwait(false);
        }
    }
}
