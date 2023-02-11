using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Commands;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Implementation.Common;

namespace UnitOfWorkScopes.Dal.Implementation.Commands
{
    public class ReserveGoodsCmdStub : DaoBaseStub, IReserveGoodsCmd
    {
        public ReserveGoodsCmdStub(IOrderStorageContext context, ILogger<ReserveGoodsCmdStub> logger) 
            : base(context, logger)
        {
        }

        public async Task ExecuteAsync(IEnumerable<Guid> cmd)
        {
            await DoSomeRequestAsync()
                .ConfigureAwait(false);
        }
    }
}
