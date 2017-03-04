using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetOrderGoodsInfoAsyncQueryStub : DaoBaseStub, IQueryAsync<GetOrderGoodsInfoAsyncQueryDto, GoodsInfoDto[]>
    {
        public GetOrderGoodsInfoAsyncQueryStub(IOrderStorageContext context, ILogger<GetOrderGoodsInfoAsyncQueryStub> logger)
            : base(context, logger)
        {
        }

        public async Task<GoodsInfoDto[]> AskAsync(GetOrderGoodsInfoAsyncQueryDto query)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);

            return new []
            {
              new GoodsInfoDto
                {
                    Id = query.OrderId,
                    Name = "Планшет Android 10 дюймов.",
                    Price = 100m
                }
            };
        }
    }
}
