using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetOrderGoodsInfoAsyncQueryStub : DaoBaseStub, IGetOrderGoodsInfoAsyncQuery
    {
        public GetOrderGoodsInfoAsyncQueryStub(IOrderStorageContext context, ILogger<GetOrderGoodsInfoAsyncQueryStub> logger)
            : base(context, logger)
        {
        }

        public async Task<GoodsInfoDto[]> AskAsync(Guid query)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);

            return new []
            {
              new GoodsInfoDto
                {
                    Id = query,
                    Name = "Планшет Android 10 дюймов.",
                    Price = 100m
                }
            };
        }
    }
}
