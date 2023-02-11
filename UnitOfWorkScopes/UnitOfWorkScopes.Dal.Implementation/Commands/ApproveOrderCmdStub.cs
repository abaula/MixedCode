using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Commands;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Implementation.Common;

namespace UnitOfWorkScopes.Dal.Implementation.Commands
{
    public class ApproveOrderCmdStub : DaoBaseStub, IApproveOrderCmd
    {
        public ApproveOrderCmdStub(IOrderStorageContext context, ILogger<ApproveOrderCmdStub> logger)
            : base(context, logger)
        {
        }

        public async Task ExecuteAsync(Guid cmd)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);
        }
    }
}
