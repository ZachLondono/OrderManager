namespace Sales.Contracts;

public class OrderDetails {

    public int Id {  get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public int VendorId { get; set; }

    public int SupplierId { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? PlacedDate { get; set; }

    public DateTime? ConfirmedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string Fields { get; set; } = string.Empty;

    public IEnumerable<int> OrderedItems { get; set; } = Enumerable.Empty<int>();

}
