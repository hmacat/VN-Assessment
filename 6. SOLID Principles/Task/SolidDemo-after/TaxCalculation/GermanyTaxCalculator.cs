namespace SolidDemo_after.TaxCalculation;

internal class GermanyTaxCalculator : ITaxCalculator
{
    public decimal Calculate(decimal amount)
    {
        return amount * 0.4m;
    }
}
