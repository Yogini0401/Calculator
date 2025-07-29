using Calculator.Data;
using Calculator.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Repository
{
    public class CalculatorRepository : ICalculatorRepository
    {
        private readonly CalculatorDbContext _dbContext;

        public CalculatorRepository(CalculatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Calculation> SaveCalculation(Calculation calculations)
        {
            _dbContext.Calculations.Add(calculations);
            await _dbContext.SaveChangesAsync();
            return calculations;
        }

        public async Task<IEnumerable<Calculation>> GetAllCalculations()
        {
            return await _dbContext.Calculations.ToListAsync();
        }
    }
}
