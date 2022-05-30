namespace Sales.Contracts;

/// <summary>
/// A simple representation of an existing order, with minimum information
/// </summary>
public class OrderSummary {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public DateTime? PlacedDate { get; set; }

    public string Status { get; set; } = string.Empty;

}