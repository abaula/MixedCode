using System.Data.SqlClient;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;

namespace UnitOfWorkScopes.Dal.Implementation.Contexts
{
    public class OrderStorageContext : StorageContextBase, IOrderStorageContext
    {
        public OrderStorageContext(IUnitOfWorkScopeProxy scope, string connectionString)
            : base(new SqlConnection(connectionString), scope.IsolationLevel)
        {
            // Контекст БД на запись поддерживает транзакции.
            scope.RegisterContext(this);
        }
    }
}
