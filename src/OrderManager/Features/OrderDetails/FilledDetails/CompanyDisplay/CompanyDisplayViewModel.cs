using Domain.Entities;
using OrderManager.Shared;

namespace OrderManager.Features.OrderDetails.FilledDetails.CompanyDisplay;

public class CompanyDisplayViewModel : ViewModelBase {

    public Company? Company { get; set; }

    public string CompanyRole { get; set; } = "Company Role";

    public string CompanyName => Company?.Name ?? "";

    public string ContactName => Company?.Contact ?? "";

    public string AddressLine1 => Company?.AddressLine1 ?? "";

    public string AddressLine2 => Company?.AddressLine2 ?? "";

    public string AddressLine3 => Company?.AddressLine3 ?? "";

    public string CityStateZip => $"{Company?.City}, {Company?.State} {Company?.Zip}";

}
