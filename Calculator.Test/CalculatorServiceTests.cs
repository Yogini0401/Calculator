using Calculator.Models;
using Calculator.Repository;
using Calculator.Services;
using Moq;

namespace Calculator.Test
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        private Mock<ICalculatorRepository> _mockRepository;
        private CalculatorService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ICalculatorRepository>();
            _service = new CalculatorService(_mockRepository.Object);
        }

        [Test]
        public async Task MultiplyAsync_WithValidInput_ReturnsCorrectResult()
        {
            var input = new double[] { 5, 3, 2 };
            var expected = new Calculation
            {
                Numbers = input.ToList(),
                Operation = "Multiply",
                CalculationResult = 30
            };

            _mockRepository.Setup(r => r.SaveCalculation(It.IsAny<Calculation>()))
                           .ReturnsAsync(expected);

            var result = await _service.MultiplyAsync(input);

            Assert.That(result.CalculationResult, Is.EqualTo(24));
            Assert.That(result.Operation, Is.EqualTo("Multiply"));
            Assert.That(result.Numbers.Count, Is.EqualTo(3));
        }

        [Test]
        public void MultiplyAsync_WithNoNumbers_ThrowsArgumentException()
        {
            double[] numbers = { };

            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.MultiplyAsync(numbers));
            Assert.That(ex.Message, Is.EqualTo("Please enter at least two numbers for multiplication."));
        }

        [Test]
        public async Task GetAllCalculationsAsync_ReturnsAllItems()
        {
            var data = new List<Calculation>
            {
                new Calculation { Id = 1, Numbers = new List<double>{ 2, 2 }, Operation = "Multiply", CalculationResult = 4 }
            };

            _mockRepository.Setup(r => r.GetAllCalculations()).ReturnsAsync(data);

            var result = await _service.GetAllCalculationsAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Operation, Is.EqualTo("Multiply"));
        }
    }
}
