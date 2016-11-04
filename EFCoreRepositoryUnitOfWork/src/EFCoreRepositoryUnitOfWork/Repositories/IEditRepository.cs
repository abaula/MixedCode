
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitOfWork.Repositories
{
    public interface IEditRepository
    {
        DbContext DbContextForTestPurpose { get; }
        void AddValue(string value);
        void DeleteAll();
    }
}