using System.Threading.Tasks;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Works;

namespace UnitOfWorkScopes.UnitOfWork.Extensions
{
    public static class UnitOfWorkScopeProxyExtension
    {
        public static async Task<TResult> DoWorkAsync<TWorkData, TResult>(this IUnitOfWorkScopeProxy scope, TWorkData workData, bool continueOnCapturedContext = false)
        {
            return await scope.Get<IWorkAsync<TWorkData, TResult>>()
                .DoAsync(workData)
                .ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task DoWorkAsync<TWorkData>(this IUnitOfWorkScopeProxy scope, TWorkData workData, bool continueOnCapturedContext = false)
        {
            await scope.Get<IWorkAsync<TWorkData>>()
                .DoAsync(workData)
                .ConfigureAwait(continueOnCapturedContext);
        }

        public static async Task ExecuteCommandAsync<TCmd>(this IUnitOfWorkScopeProxy scope, TCmd cmd, bool continueOnCapturedContext = false)
        {
            await scope.Get<ICommandAsync<TCmd>>()
                .ExecuteAsync(cmd)
                .ConfigureAwait(continueOnCapturedContext);
        }
    }
}
