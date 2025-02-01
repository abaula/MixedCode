using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;

namespace UnitOfWorkScopes.Dal.Implementation.Common
{
    /// <summary>
    /// Реализация физического доступа к хранилищу данных.
    /// С использованием Dapper.
    /// </summary>
    public abstract class DaoBase
    {
        private readonly IConnectionContext _connectionContext;

        protected DaoBase(IConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        protected DynamicParameters CreateDynamicParameters()
        {
            return new DynamicParameters();
        }

        protected async Task<IEnumerable<T>> GetAsync<T>(string spName, DynamicParameters parameters = null)
        {
            var contextData = await _connectionContext.GetContextDataAsync()
                .ConfigureAwait(false);

            return await contextData.Connection.QueryAsync<T>(spName,
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: contextData.Transaction)
                .ConfigureAwait(false);
        }

        protected async Task<int> ExecuteAsync(string spName, DynamicParameters parameters = null)
        {
            var contextData = await _connectionContext.GetContextDataAsync()
                .ConfigureAwait(false);

            return await contextData.Connection.ExecuteAsync(spName,
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: contextData.Transaction)
                .ConfigureAwait(false);
        }

        protected async Task<T> ExecuteScalarAsync<T>(string spName, DynamicParameters parameters = null)
        {
            var contextData = await _connectionContext.GetContextDataAsync()
                .ConfigureAwait(false);

            return await contextData.Connection.ExecuteScalarAsync<T>(spName,
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: contextData.Transaction)
                .ConfigureAwait(false);
        }

        protected async Task<T> ReadMultipleAsync<T>(string spName, DynamicParameters parameters, Func<SqlMapper.GridReader, Task<T>> funcAsync)
        {
            var contextData = await _connectionContext.GetContextDataAsync()
                .ConfigureAwait(false);

            var reader = await contextData.Connection.QueryMultipleAsync(spName,
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: contextData.Transaction)
                .ConfigureAwait(false);

            return await funcAsync(reader)
                .ConfigureAwait(false);
        }
    }
}
