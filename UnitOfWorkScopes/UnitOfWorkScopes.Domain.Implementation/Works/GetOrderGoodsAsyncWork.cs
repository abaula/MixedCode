using System;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Queries;
using UnitOfWorkScopes.Domain.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class GetOrderGoodsAsyncWork : IGetOrderGoodsAsyncWork
    {
        private readonly IGetOrderGoodsInfoAsyncQuery _query;

        public GetOrderGoodsAsyncWork(IGetOrderGoodsInfoAsyncQuery query)
        {
            _query = query;
        }

        public async Task<GoodsInfoDto[]> DoAsync(Guid data)
        {
            return await _query.AskAsync(data)
                .ConfigureAwait(false);
        }
    }
}
