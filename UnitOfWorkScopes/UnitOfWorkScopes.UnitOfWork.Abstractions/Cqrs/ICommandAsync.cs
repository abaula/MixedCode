using System.Threading.Tasks;

namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs
{
    public interface ICommandAsync<in TCmd>
    {
        Task ExecuteAsync(TCmd cmd);
    }
}
