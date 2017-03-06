using System;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Abstractions.Commands
{
    public interface IApproveOrderCmd : ICommandAsync<Guid>
    {
    }
}
