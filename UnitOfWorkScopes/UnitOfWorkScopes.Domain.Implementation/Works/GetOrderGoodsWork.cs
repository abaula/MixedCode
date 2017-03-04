using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Queries;
using UnitOfWorkScopes.Domain.Abstractions.Works;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class GetOrderGoodsWork : IWorkAsync<GetOrderGoodsWorkDto, GoodsInfoDto[]>
    {
        private readonly IQueryAsync<GetOrderGoodsInfoAsyncQueryDto, GoodsInfoDto[]> _query;

        public GetOrderGoodsWork(IQueryAsync<GetOrderGoodsInfoAsyncQueryDto, GoodsInfoDto[]> query)
        {
            _query = query;
        }

        public async Task<GoodsInfoDto[]> DoAsync(GetOrderGoodsWorkDto data)
        {
            return await _query.AskAsync(new GetOrderGoodsInfoAsyncQueryDto { OrderId = data.OrderId })
                .ConfigureAwait(false);
        }
    }
}
