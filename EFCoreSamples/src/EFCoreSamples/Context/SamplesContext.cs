using EFCoreSamples.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSamples.Context
{
    public class SamplesContext : DbContext
    {
        public SamplesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SampleEntry> SampleEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
