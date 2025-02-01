using System;
using System.Threading.Tasks;

namespace UnitOfWorkScopes.Dal.Abstractions.Contexts
{
    public interface IConnectionContext : IDisposable
    {
        Task<IConnectionContextData> GetContextDataAsync();
    }
}
