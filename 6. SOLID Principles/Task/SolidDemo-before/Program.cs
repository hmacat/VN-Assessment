namespace SolidDemo_before
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var taxCalculator = new TaxCalculator();
            var taxResult = taxCalculator.Calculate();

            Console.WriteLine($"Calculated Tax: {taxResult}");
        }
    }
}
