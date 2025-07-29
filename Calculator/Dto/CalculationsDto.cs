using Calculator.Models;

namespace Calculator.Dto
{
    public class CalculationsDto
    {
        public int Id { get; set; }
        public List<double> Numbers { get; set; }
        public double CalculationResult { get; set; }
        public string Operation { get; set; }

        public CalculationsDto() { }

        public CalculationsDto(Calculation calculations)
        {
            Id = calculations.Id;
            Numbers = calculations.Numbers;
            Operation = calculations.Operation;
            CalculationResult = calculations.CalculationResult;
        }
    }
}
