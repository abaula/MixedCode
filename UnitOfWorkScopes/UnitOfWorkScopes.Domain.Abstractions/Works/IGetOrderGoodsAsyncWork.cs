using System;
using UnitOfWorkScopes.Dal.Abstractions.Dtos;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Abstractions.Works
{
    public interface IGetOrderGoodsAsyncWork : IWorkAsync<Guid, GoodsInfoDto[]>
    {
    }
}
