using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitOfWork.Repositories
{
    public interface IReadOnlyRepository
    {
        DbContext DbContextForTestPurpose { get; }
        IEnumerable<string> GetAllValues();
    }
}