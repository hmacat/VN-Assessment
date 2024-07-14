using SolidDemo_after.Notification;

namespace SolidDemo_after;

internal class Program
{
    static void Main(string[] args)
    {
        var taxCalculator = new TaxService(new JsonTaxProvider(), new MailNotificationService());
        var taxResult = taxCalculator.Calculate();

        Console.WriteLine($"Calculated Tax: {taxResult}");
    }
}
