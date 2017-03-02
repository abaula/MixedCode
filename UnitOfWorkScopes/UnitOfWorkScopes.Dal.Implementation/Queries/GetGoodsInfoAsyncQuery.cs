using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Queries;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Implementation.Queries
{
    public class GetGoodsInfoAsyncQuery : DaoBase, IQueryAsync<GetGoodsInfoAsyncQueryDto, GoodsInfoDto>
    {
        public GetGoodsInfoAsyncQuery(IOrderStorageContext context)
            : base(context)
        {
        }

        public async Task<GoodsInfoDto> AskAsync(GetGoodsInfoAsyncQueryDto query)
        {
            var parameters = CreateDynamicParameters();
            parameters.Add("@goodsId", query.GoodsId);

            return (await GetAsync<GoodsInfoDto>("dbo.getGoodsInfo", parameters)
                .ConfigureAwait(false))
                .SingleOrDefault();
        }
    }
}
