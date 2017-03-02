
namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Works
{
    public interface IWork
    {
        void Do();
    }

    public interface IWork<in TData>
    {
        void Do(TData data);
    }

    public interface IWork<in TData, out TResult>
    {
        TResult Do(TData data);
    }
}
