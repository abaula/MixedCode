using System.Data.Common;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;

namespace UnitOfWorkScopes.Dal.Implementation.Common
{
    public class ConnectionContextData : IConnectionContextData
    {
        public ConnectionContextData(DbConnection connection, DbTransaction transaction = null)
        {
            Connection = connection;
            Transaction = transaction;
        }

        public DbConnection Connection { get; }
        public DbTransaction Transaction { get; set; }
    }
}
