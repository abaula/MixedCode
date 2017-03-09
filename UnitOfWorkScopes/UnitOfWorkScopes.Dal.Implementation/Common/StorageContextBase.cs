using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Contexts;

namespace UnitOfWorkScopes.Dal.Implementation.Common
{
    public abstract class StorageContextBase : IConnectionContext, ISharedContext
    {
        private readonly ConnectionContextData _connectionContextData;
        private readonly IsolationLevel _isolationLevel;
        private readonly SemaphoreSlim _semaphoreSlim;

        protected StorageContextBase(DbConnection connection, IsolationLevel isolationLevel)
        {
            _connectionContextData = new ConnectionContextData(connection);
            _isolationLevel = isolationLevel;
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        public void Commit()
        {
            _connectionContextData.Transaction?.Commit();
        }

        public void Rollback()
        {
            _connectionContextData.Transaction?.Rollback();
        }

        public void Dispose()
        {
            _connectionContextData.Connection?.Dispose();
        }

        public async Task<IConnectionContextData> GetContextDataAsync()
        {
            await TryOpenConnectionAsync()
                .ConfigureAwait(false);

            return _connectionContextData;
        }

        private async Task TryOpenConnectionAsync()
        {
            if (_connectionContextData.Connection.State != ConnectionState.Closed)
                return;

            try
            {
                await _semaphoreSlim.WaitAsync();

                if (_connectionContextData.Connection.State != ConnectionState.Closed)
                    return;

                await _connectionContextData.Connection.OpenAsync()
                    .ConfigureAwait(false);

                if (_isolationLevel == IsolationLevel.Unspecified)
                    return;

                _connectionContextData.Transaction = _connectionContextData.Connection.BeginTransaction(_isolationLevel);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
