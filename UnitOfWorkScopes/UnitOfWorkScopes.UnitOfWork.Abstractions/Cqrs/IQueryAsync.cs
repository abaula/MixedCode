using System.Threading.Tasks;

namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs
{
    public interface IQueryAsync<TResult>
    {
        Task<TResult> AskAsync();
    }

    public interface IQueryAsync<in TQuery, TResult>
    {
        Task<TResult> AskAsync(TQuery query);
    }
}
