using System;
using System.Data.SqlClient;
using System.Linq;
using EFCoreSamples.Context;
using EFCoreSamples.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreSamples
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //InsertSampleValues();
            TestTransactions();
            OutputSampleValues();
            ClearAllValues();

            Console.ReadKey();
        }

        private static void TestTransactions()
        {
            using (var repo = new Repository(Configuration["ConnectionString"]))
            {
                repo.AddValue("Первое значение без транзакции");
                repo.AddValue("Второе значение без транзакции");

                repo.BeginTran();
                repo.AddValue("Не сохраняем 1");
                repo.AddValue("Не сохраняем 2");
                repo.RollbackTran();

                repo.AddValue("Третье значение без транзакции");

                repo.BeginTran();
                repo.AddValue("Сохраняем в транзакции 1");
                repo.AddValue("Сохраняем в транзакции 2");
                repo.CommitTran();

                repo.AddValue("Четвёртое значение без транзакции");

                repo.BeginTran();
                repo.AddValue("Не сохраниться 1");
                repo.AddValue("Не сохраниться 2");
            }
        }

        private static void InsertSampleValues()
        {
            using (var ctx = new SamplesContext(GetOptions()))
            {
                ctx.SampleEntries.Add(new SampleEntry {Value = $"Value at {DateTime.Now}"});
                ctx.SaveChanges();
            }
        }

        private static void ClearAllValues()
        {
            using (var ctx = new SamplesContext(GetOptions()))
            {
                ctx.Database.ExecuteSqlCommand("DELETE FROM [dbo].[SampleEntry]");
            }
        }

        private static void OutputSampleValues()
        {
            using (var ctx = new SamplesContext(GetOptions()))
            {
                var values = ctx.SampleEntries.AsNoTracking().ToArray();

                foreach (var entity in values)
                {
                    Console.WriteLine($"{entity.Id} - {entity.Value}");
                }
            }
        }

        private static DbContextOptions<SamplesContext> GetOptions()
        {
            return new DbContextOptionsBuilder<SamplesContext>()
                .UseSqlServer(new SqlConnection(Configuration["ConnectionString"]))
                .Options;
        }
    }
}
