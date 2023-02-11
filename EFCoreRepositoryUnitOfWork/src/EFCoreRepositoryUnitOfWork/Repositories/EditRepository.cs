using EFCoreRepositoryUnitOfWork.Contexts;
using EFCoreRepositoryUnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitOfWork.Repositories
{
    public class EditRepository : IEditRepository
    {
        private readonly SampleContext _context;

        public EditRepository(SampleContext context)
        {
            _context = context;
        }

        public DbContext DbContextForTestPurpose => _context;

        public void AddValue(string value)
        {
            _context.SampleEntries.Add(new SampleEntry { Value = value });
        }

        public void DeleteAll()
        {            
            _context.Database.ExecuteSqlCommand("DELETE FROM [dbo].[SampleEntry]");
        }
    }
}
