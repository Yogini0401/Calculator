using Calculator.Controllers;
using Calculator.Dto;
using Calculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Calculator.Tests.Controllers
{
    [TestFixture]
    public class CalculatorControllerTests
    {
        private Mock<ICalculatorService> _calculatorServiceMock;
        private Mock<ILogger<CalculatorController>> _loggerMock;
        private CalculatorController _controller;

        [SetUp]
        public void Setup()
        {
            _calculatorServiceMock = new Mock<ICalculatorService>();
            _loggerMock = new Mock<ILogger<CalculatorController>>();
            _controller = new CalculatorController(_calculatorServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Multiply_ValidNumbers_ReturnsOkWithResult()
        {
            var numbers = new double[] { 3, 3, 10 };
            var expectedDto = new CalculationsDto
            {
                Numbers = numbers.ToList(),
                Operation = "Multiply",
                CalculationResult = 90
            };

            _calculatorServiceMock.Setup(s => s.MultiplyAsync(numbers))
                .ReturnsAsync(expectedDto);

            var result = await _controller.Multiply(numbers);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            var dto = okResult.Value as CalculationsDto;
            Assert.IsNotNull(dto, "Expected the response body to be of type CalculationsDto, but it was null or of a different type.");
            Assert.That(dto.CalculationResult, Is.EqualTo(90));
        }


        [Test]
        public async Task Multiply_WithNoNumbers_ReturnsBadRequest()
        {
            double[] numbers = { };

            var result = await _controller.Multiply(numbers) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("Please enter at least two numbers for multiplication."));
        }

        [Test]
        public async Task GetAllCalculations_ReturnsAllItems()
        {
            var dtos = new List<CalculationsDto>
            {
                new CalculationsDto { CalculationResult = 5 },
                new CalculationsDto { CalculationResult = 100 }
            };

            _calculatorServiceMock.Setup(s => s.GetAllCalculationsAsync())
                .ReturnsAsync(dtos);

            var result = await _controller.GetAllCalculations();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDtos = okResult.Value as IEnumerable<CalculationsDto>;
            Assert.IsNotNull(returnedDtos, "Expected OkObjectResult.Value to be an IEnumerable<CalculationsDto>.");
            Assert.That(returnedDtos.Count(), Is.EqualTo(2), "Expected 2 CalculationsDto items in the response.");
        }
    }
}

