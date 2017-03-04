using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;

namespace UnitOfWorkScopes.Dal.Implementation.Common
{
    public abstract class DaoBaseStub
    {
        private readonly IConnectionContext _connectionContext;

        protected DaoBaseStub(IConnectionContext connectionContext, ILogger logger)
        {
            _connectionContext = connectionContext;
            Logger = logger;
        }

        protected ILogger Logger { get; }

        public async Task DoSomeRequestAsync()
        {
            Logger.LogTrace("Start => DaoBaseStub.DoSomeRequestAsync()");
            Logger.LogInformation("IConnectionContext.GetHashCode() = {0}.", _connectionContext.GetHashCode());
            Logger.LogTrace("End => DaoBaseStub.DoSomeRequestAsync()");
        }
    }
}
