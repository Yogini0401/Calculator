namespace Calculator.Models
{
    public class Calculation
    {
        public int Id { get; set; }
        public List<double> Numbers { get; set; }
        public string Operation { get; set; }
        public double CalculationResult { get; set; }
    }
}
