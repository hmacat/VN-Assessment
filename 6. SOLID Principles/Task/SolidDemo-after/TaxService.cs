using SolidDemo_after.Interfaces;
using SolidDemo_after.TaxCalculation;

namespace SolidDemo_after;

public class TaxService
{
    private readonly ITaxProvider _taxProvider;
    private readonly IMailNotificationService _mailNotificationService;

    public TaxService(ITaxProvider taxProvider, IMailNotificationService mailService)
    {
        //Required dependencies are injected via constructor.
        _taxProvider = taxProvider;
        _mailNotificationService = mailService;
    }

    public decimal Calculate()
    {
        //Tax reading responsibility now belongs to ITaxProvider, applying SRP.
        var tax = _taxProvider.GetTax();
        if (tax == null)
            return 0;

        //Tax calculation logic now follows OCP.
        // We don't have to modify the code in case of adding new countries anymore.
        var taxCalculatorFactory = new TaxCalculatorFactory();
        var taxCalculator = taxCalculatorFactory.Create(tax.Type);
        var calculatedTax = taxCalculator.Calculate(tax.Amount);

        //Calculation code does not depend on mail notification implementation anymore,
        //which makes unit testing easier. DIP is applied.
        _mailNotificationService.Send($"Tax calculation report. Tax Amount: {calculatedTax}");

        return calculatedTax;
    }
}