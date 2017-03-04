using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetOrderAmountAsyncQueryStub : DaoBaseStub, IQueryAsync<GetOrderAmountAsyncQueryDto, decimal>
    {
        public GetOrderAmountAsyncQueryStub(IOrderStorageContext context, ILogger<GetOrderAmountAsyncQueryStub> logger) 
            : base(context, logger)
        {
        }

        public async Task<decimal> AskAsync(GetOrderAmountAsyncQueryDto query)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);

            return 200m;
        }
    }
}
