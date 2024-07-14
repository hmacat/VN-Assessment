using Newtonsoft.Json;

namespace SolidDemo_after;

internal class JsonTaxProvider : ITaxProvider
{
    public Tax GetTax()
    {
        var taxJson = File.ReadAllText("tax.json");
        return JsonConvert.DeserializeObject<Tax>(taxJson);
    }
}
