using Calculator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Calculator.Data
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options)
        {
        }

        public DbSet<Calculation> Calculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calculation>()
                .HasKey(t => t.Id);

            var doubleListConverter = new ValueConverter<List<double>, string>(
                numbersObject => JsonConvert.SerializeObject(numbersObject ?? new List<double>()),
                serializedString => JsonConvert.DeserializeObject<List<double>>(serializedString) ?? new List<double>()
            );

            modelBuilder.Entity<Calculation>()
            .Property(c => c.Numbers)
            .HasConversion(doubleListConverter);    

            base.OnModelCreating(modelBuilder);
        }
    }
}
