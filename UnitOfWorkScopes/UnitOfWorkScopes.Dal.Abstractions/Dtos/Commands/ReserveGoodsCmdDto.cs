using System;
using System.Collections.Generic;

namespace UnitOfWorkScopes.Dal.Abstractions.Dtos.Commands
{
    public class ReserveGoodsCmdDto
    {
        public IEnumerable<Guid> GoodsIds { get; set; }
    }
}
