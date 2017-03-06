using System;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Abstractions.Queries
{
    public interface IGetOrderGoodsInfoAsyncQuery : IQueryAsync<Guid, GoodsInfoDto[]>
    {
    }
}
