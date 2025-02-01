using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSamples
{
    public class RepositoryBase<TContext> : IDisposable where TContext : DbContext
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public RepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
            _connection = null;
            _transaction = null;
        }

        public void BeginTran()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Транзакция уже начата. Вложенные транзакции не поддерживаются.");

            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTran()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Транзакция отсутствует.");

            _transaction.Commit();
            _connection.Close();
            _transaction = null;
            _connection = null;            
        }

        public void RollbackTran()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Транзакция отсутствует.");

            _transaction.Rollback();
            _connection.Close();
            _transaction = null;
            _connection = null;
        }

        protected DbContextOptions<TContext> GetOptions()
        {
            return new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(_connection ?? new SqlConnection(_connectionString))
                .Options;
        }

        protected void InitializeContext(DbContext context)
        {
            if (_transaction == null)
                return;

            context.Database.UseTransaction(_transaction);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
