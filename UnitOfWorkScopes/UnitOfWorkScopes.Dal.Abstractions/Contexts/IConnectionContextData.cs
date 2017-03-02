using System.Data.Common;

namespace UnitOfWorkScopes.Dal.Abstractions.Contexts
{
    public interface IConnectionContextData
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
    }
}
