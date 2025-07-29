using Calculator.Dto;
using Calculator.Models;
using Calculator.Repository;

namespace Calculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculatorRepository _calculatorRepository;

        public CalculatorService(ICalculatorRepository calculatorRepository)
        {
            _calculatorRepository = calculatorRepository;
        }
        public async Task<CalculationsDto> MultiplyAsync(params double[] numbers)
        {
            double result = 1.0;           

            if (numbers.Length <= 1)
                throw new ArgumentException("Please enter at least two numbers for multiplication.");

            foreach (var number in numbers)
            {
                result *= number;
            }

            var calculation = new Calculation
            {
                Numbers = numbers.ToList(),
                Operation = OperationType.Multiply.ToString(),
                CalculationResult = result
            };

            var returnCal = await _calculatorRepository.SaveCalculation(calculation);
            return new CalculationsDto(returnCal);
        }

        public async Task<IEnumerable<CalculationsDto>> GetAllCalculationsAsync()
        {
            var returnCal = await _calculatorRepository.GetAllCalculations();
            return returnCal.Select(cal => new CalculationsDto(cal));
        }
    }
}
