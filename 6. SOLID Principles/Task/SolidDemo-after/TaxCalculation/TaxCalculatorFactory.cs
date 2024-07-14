namespace SolidDemo_after.TaxCalculation;

internal class TaxCalculatorFactory
{
    internal ITaxCalculator Create(TaxCalculatorType taxCalculationType)
    {
        return (ITaxCalculator)Activator.CreateInstance(
            Type.GetType($"SolidDemo_after.TaxCalculation.{taxCalculationType}"));
    }
}
