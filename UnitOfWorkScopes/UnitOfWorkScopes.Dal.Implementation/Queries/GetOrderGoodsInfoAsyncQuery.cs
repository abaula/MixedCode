using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetOrderGoodsInfoAsyncQuery : DaoBase, IQueryAsync<GetOrderGoodsInfoAsyncQueryDto, GoodsInfoDto[]>
    {
        public GetOrderGoodsInfoAsyncQuery(IOrderStorageContext context)
            : base(context)
        {
        }

        public async Task<GoodsInfoDto[]> AskAsync(GetOrderGoodsInfoAsyncQueryDto query)
        {
            var parameters = CreateDynamicParameters();
            parameters.Add("@orderId", query.OrderId);

            return (await GetAsync<GoodsInfoDto>("dbo.getOrderGoodsInfo", parameters)
                .ConfigureAwait(false))
                .ToArray();
        }
    }
}
