namespace SolidDemo_after.TaxCalculation;

internal class TurkeyTaxCalculator : ITaxCalculator
{
    public decimal Calculate(decimal amount)
    {
        return amount * 0.35m;
    }
}
