namespace SolidDemo_after.TaxCalculation;

internal class UsTaxCalculator : ITaxCalculator
{
    public decimal Calculate(decimal amount)
    {
        return amount * 0.32m;
    }
}
