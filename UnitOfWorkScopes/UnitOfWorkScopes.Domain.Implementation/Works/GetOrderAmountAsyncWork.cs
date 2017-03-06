using System;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Queries;
using UnitOfWorkScopes.Domain.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class GetOrderAmountAsyncWork : IGetOrderAmountAsyncWork
    {
        private readonly IGetOrderAmountAsyncQuery _query;

        public GetOrderAmountAsyncWork(IGetOrderAmountAsyncQuery query)
        {
            _query = query;
        }

        public async Task<decimal> DoAsync(Guid data)
        {
            return await _query.AskAsync(data)
                .ConfigureAwait(false);
        }
    }
}
