using Calculator.Dto;

namespace Calculator.Services
{
    public interface ICalculatorService
    {
        public Task<CalculationsDto> MultiplyAsync(params double[] numbers);
        public Task<IEnumerable<CalculationsDto>> GetAllCalculationsAsync();
    }
}
