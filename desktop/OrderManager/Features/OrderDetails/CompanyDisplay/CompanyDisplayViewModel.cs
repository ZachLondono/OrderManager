using OrderManager.Shared;

namespace OrderManager.Features.OrderDetails.CompanyDisplay;

public class CompanyDisplayViewModel : ViewModelBase {

    public CompanyDetails? Company { get; set; }

    public string CompanyRole { get; set; } = "Company Role";

    public string CompanyName => Company?.Name ?? "";

    public string ContactName => Company?.Contact ?? "";

    public string AddressLine1 => Company?.Address1 ?? "";

    public string AddressLine2 => Company?.Address2 ?? "";

    public string AddressLine3 => Company?.Address3 ?? "";

    public string CityStateZip => $"{Company?.City}{(string.IsNullOrEmpty(Company?.City) ? "" : ",")} {Company?.State} {Company?.Zip}";

}
