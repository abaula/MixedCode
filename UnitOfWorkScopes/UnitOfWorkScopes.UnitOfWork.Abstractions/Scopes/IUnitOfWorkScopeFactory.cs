using System.Data;

namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes
{
    public interface IUnitOfWorkScopeFactory
    {
        IUnitOfWorkScopeProxy Create(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
    }
}
