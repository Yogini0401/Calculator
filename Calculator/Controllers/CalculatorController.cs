using Calculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        public ILogger<CalculatorController> _logger;

        public CalculatorController(ICalculatorService calculatorService, ILogger<CalculatorController> logger)
        {
            _calculatorService = calculatorService;
            _logger = logger;
        }

        [HttpPost("multiply")]
        public async Task<IActionResult> Multiply(params double[] numbers)
        {
            try
            {
                if (numbers.Length <= 1)
                    return BadRequest("Please enter at least two numbers for multiplication.");

                var result = await _calculatorService.MultiplyAsync(numbers);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}, {ex.InnerException?.Message}");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCalculations()
        {
            var allCalculations = await _calculatorService.GetAllCalculationsAsync();
            return Ok(allCalculations);
        }
    }
}
