using EFCoreRepositoryUnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitOfWork.Contexts
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }

        public DbSet<SampleEntry> SampleEntries { get; set; }
    }
}
