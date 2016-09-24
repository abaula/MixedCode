
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitOfWork
{
    public interface IEditRepository
    {
        DbContext DbContextForTestPurpose { get; }
        void AddValue(string value);
        void DeleteAll();
    }
}