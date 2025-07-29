using Calculator.Models;

namespace Calculator.Repository
{
    public interface ICalculatorRepository
    {
        public Task<Calculation> SaveCalculation(Calculation calculation);
        public Task<IEnumerable<Calculation>> GetAllCalculations();
    }
}
