namespace Sales.Contracts;

/// <summary>
/// Represents the details of an existing order
/// </summary>
public class OrderDetails {

    public int Id {  get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public CompanySummary? Customer { get; set; }

    public CompanySummary? Vendor { get; set; }

    public CompanySummary? Supplier { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? PlacedDate { get; set; }

    public DateTime? ConfirmedDate { get; set; }

    public DateTime? ReleasedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string Info { get; set; } = string.Empty;

    public IEnumerable<OrderedItemDetails> OrderedItems { get; set; } = Enumerable.Empty<OrderedItemDetails>();

}
