using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Dtos.Commands;
using UnitOfWorkScopes.Dal.Implementation.Common;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Cqrs;

namespace UnitOfWorkScopes.Dal.Implementation.Commands
{
    public class ApproveOrderCmdStub : DaoBaseStub, ICommandAsync<ApproveOrderCmdDto>
    {
        public ApproveOrderCmdStub(IOrderStorageContext context, ILogger<ApproveOrderCmdStub> logger)
            : base(context, logger)
        {
        }

        public async Task ExecuteAsync(ApproveOrderCmdDto cmd)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);
        }
    }
}
