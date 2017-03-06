using System;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Abstractions.Queries
{
    public interface IGetOrderAmountAsyncQuery : IQueryAsync<Guid, decimal>
    {
    }
}
