using Newtonsoft.Json;

namespace SolidDemo_before;

public class TaxCalculator
{
    public decimal Calculate()
    {
        // This class has more then one responsibility. It reads the tax data from file,
        // then converts it to an actual tax instance. After that, it performs tax calculation.
        // We should move these responsibilities to seperate classes to apply SRP.
        // Also new interfaces will be implemented for reading the text and json conversion to support DIP.
        var taxJson = File.ReadAllText("tax.json");
        var tax = JsonConvert.DeserializeObject<Tax>(taxJson);

        if (tax == null)
            return 0;

        decimal calculatedTax = 0;

        //This code violates the OCP. For each country that may be added in the future, we have to modify this code.
        //We should refactor it.
        switch (tax.Type)
        {
            case TaxCalculationType.GermanyTaxCalculation:
                calculatedTax = tax.Amount * 0.4m;
                break;
            case TaxCalculationType.UsTaxCalculation:
                calculatedTax = tax.Amount * 0.32m;
                break;
            case TaxCalculationType.TurkeyTaxCalculation:
                calculatedTax = tax.Amount * 0.35m;
                break;

        }

        //This dependency should be injected from constructor to support DIP.
        var notificationService = new NotificationService();
        notificationService.SendMailNotification($"Tax calculation report. Tax Amount: {calculatedTax}");

        return calculatedTax;
    }
}