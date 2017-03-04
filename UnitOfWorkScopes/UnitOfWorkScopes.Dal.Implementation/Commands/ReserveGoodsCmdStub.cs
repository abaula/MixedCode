using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Commands;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Implementation.Commands
{
    public class ReserveGoodsCmdStub : DaoBaseStub, ICommandAsync<ReserveGoodsCmdDto>
    {
        public ReserveGoodsCmdStub(IOrderStorageContext context, ILogger<ReserveGoodsCmdStub> logger) 
            : base(context, logger)
        {
        }

        public async Task ExecuteAsync(ReserveGoodsCmdDto cmd)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);
        }
    }
}
