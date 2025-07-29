using Calculator.Data;
using Calculator.Models;
using Calculator.Repository;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Tests.Repository
{
    [TestFixture]
    public class CalculatorRepositoryTests
    {
        private CalculatorDbContext _dbContext;
        private CalculatorRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CalculatorDbContext>()
                .UseInMemoryDatabase(databaseName: "CalculatorDb_Test")
                .Options;

            _dbContext = new CalculatorDbContext(options);
            _dbContext.Database.EnsureCreated();

            _repository = new CalculatorRepository(_dbContext);
        }

        [TearDown]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task SaveCalculation_SavesCalculationSuccessfully()
        {
            var calculation = new Calculation
            {
                Numbers = new List<double> { 2, 3 },
                Operation = "Multiply",
                CalculationResult = 6
            };

            var result = await _repository.SaveCalculation(calculation);

            Assert.NotNull(result);
            Assert.That(result.CalculationResult, Is.EqualTo(6));
            Assert.That(_dbContext.Calculations.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllCalculations_ReturnsSavedCalculations()
        {
            var calculation1 = new Calculation { Numbers = new List<double> { 4, 4 }, Operation = "Multiply", CalculationResult = 16 };
            var calculation2 = new Calculation { Numbers = new List<double> { 7, 7 }, Operation = "Multiply", CalculationResult = 49 };

            await _repository.SaveCalculation(calculation1);
            await _repository.SaveCalculation(calculation2);

            var result = (await _repository.GetAllCalculations()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.IsTrue(result.Any(c => c.CalculationResult == 16));
            Assert.IsTrue(result.Any(c => c.CalculationResult == 49));
        }
    }
}
