using System;
using System.Collections.Generic;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Abstractions.Commands
{
    public interface IReserveGoodsCmd : ICommandAsync<IEnumerable<Guid>>
    {
    }
}
