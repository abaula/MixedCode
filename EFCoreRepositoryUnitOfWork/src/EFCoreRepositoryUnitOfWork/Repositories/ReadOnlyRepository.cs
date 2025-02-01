using System.Collections.Generic;
using System.Linq;
using EFCoreRepositoryUnitOfWork.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitOfWork.Repositories
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        private readonly SampleContext _context;

        public ReadOnlyRepository(SampleContext context)
        {
            _context = context;
        }

        public DbContext DbContextForTestPurpose => _context;

        public IEnumerable<string> GetAllValues()
        {
            return _context.SampleEntries.AsNoTracking()
                .Select(e => e.Value)
                .ToArray();
        }
    }
}
