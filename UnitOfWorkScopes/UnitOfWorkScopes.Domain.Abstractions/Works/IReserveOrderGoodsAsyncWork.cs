using System;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Abstractions.Works
{
    public interface IReserveOrderGoodsAsyncWork : IWorkAsync<Guid>
    {
    }
}
