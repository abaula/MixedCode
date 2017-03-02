using System.Threading.Tasks;

namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Works
{
    public interface IWorkAsync
    {
        Task DoAsync();
    }

    public interface IWorkAsync<in TData>
    {
        Task DoAsync(TData data);
    }

    public interface IWorkAsync<in TData, TResult>
    {
        Task<TResult> DoAsync(TData data);
    }
}
