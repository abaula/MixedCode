using System.Collections.Generic;
using EFCoreRepositoryUnitOfWork.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreRepositoryUnitOfWork
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
