using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetOrderAmountAsyncQueryStub : DaoBaseStub, IGetOrderAmountAsyncQuery
    {
        public GetOrderAmountAsyncQueryStub(IOrderStorageContext context, ILogger<GetOrderAmountAsyncQueryStub> logger) 
            : base(context, logger)
        {
        }

        public async Task<decimal> AskAsync(Guid query)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);

            return 200m;
        }
    }
}
