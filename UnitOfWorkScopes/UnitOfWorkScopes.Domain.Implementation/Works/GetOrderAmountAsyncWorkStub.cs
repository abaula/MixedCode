using System;
using System.Threading.Tasks;
using UnitOfWorkScopes.Domain.Abstractions.Works;

namespace UnitOfWorkScopes.Domain.Implementation.Works
{
    public class GetOrderAmountAsyncWorkStub : IGetOrderAmountAsyncWork
    {
        public async Task<decimal> DoAsync(Guid data)
        {
            throw new NotImplementedException();
        }
    }
}
