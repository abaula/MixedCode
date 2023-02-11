using System;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetOrderGoodsInfoAsyncQuery : DaoBase, IGetOrderGoodsInfoAsyncQuery
    {
        public GetOrderGoodsInfoAsyncQuery(IOrderStorageContext context)
            : base(context)
        {
        }

        public async Task<GoodsInfoDto[]> AskAsync(Guid query)
        {
            var parameters = CreateDynamicParameters();
            parameters.Add("@orderId", query);

            return (await GetAsync<GoodsInfoDto>("dbo.getOrderGoodsInfo", parameters)
                .ConfigureAwait(false))
                .ToArray();
        }
    }
}
