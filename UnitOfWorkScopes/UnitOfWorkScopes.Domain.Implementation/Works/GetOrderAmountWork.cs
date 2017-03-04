using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Queries;
using UnitOfWorkScopes.Domain.Abstractions.Works;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class GetOrderAmountWork : IWorkAsync<GetOrderAmountWorkDto, decimal>
    {
        private readonly IQueryAsync<GetOrderAmountAsyncQueryDto, decimal> _query;

        public GetOrderAmountWork(IQueryAsync<GetOrderAmountAsyncQueryDto, decimal> query)
        {
            _query = query;
        }

        public async Task<decimal> DoAsync(GetOrderAmountWorkDto data)
        {
            return await _query.AskAsync(new GetOrderAmountAsyncQueryDto {OrderId = data.OrderId})
                .ConfigureAwait(false);
        }
    }
}
