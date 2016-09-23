using EFCoreSamples.Context;
using EFCoreSamples.Entities;

namespace EFCoreSamples
{
    public class Repository : RepositoryBase<SamplesContext>
    {
        public Repository(string connectionString)
            : base(connectionString)
        {
        }

        public void AddValue(string value)
        {
            using (var ctx = GetContext())
            {
                ctx.SampleEntries.Add(new SampleEntry { Value = value });
                ctx.SaveChanges();
            }
        }

        private SamplesContext GetContext()
        {
            var ctx = new SamplesContext(GetOptions());
            InitializeContext(ctx);
            return ctx;
        }
    }
}
