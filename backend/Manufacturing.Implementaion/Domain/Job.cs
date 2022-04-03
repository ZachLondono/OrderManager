namespace Manufacturing.Implementation.Domain;

public enum ManufacturingStatus {
    Pending,
    InProgress,
    Completed,
    Shipped,
    Canceled
}

public class Job {

    public int Id { get; init; }

    public string Name { get; private set; }

    public string Number { get; private set; }

    public int CustomerId { get; set; }

    public int VendorId { get; set; }

    public int ItemCount { get; private set; }

    public DateTime? ReleasedDate { get; private set; } = null;

    public DateTime? CompletedDate { get; private set; } = null;

    public DateTime? ShippedDate { get; private set; } = null;

    public ManufacturingStatus Status { get; private set; }

    public Job(int id, string name, string number, int customerId, int vendorId, int itemCount) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Id = id;
        Name = name;
        Number = number;
        CustomerId = customerId;
        VendorId = vendorId;
        ItemCount = itemCount;
        Status = ManufacturingStatus.Pending;
    }

    public Job(int id, string name, string number, int customerId, int vendorId, int itemCount, DateTime? releasedDate, DateTime? completedDate, DateTime? shippedDate, ManufacturingStatus status) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Id = id;
        Name = name;
        Number = number;
        CustomerId = customerId;
        VendorId = vendorId;
        ItemCount = itemCount;
        ReleasedDate = releasedDate;
        CompletedDate = completedDate;
        ShippedDate = shippedDate;
        Status = status;
    }

    public void ReleaseToProduction() {
        ReleasedDate = DateTime.Now;
        Status = ManufacturingStatus.InProgress;
    }

    public void Complete() {
        if (ReleasedDate is null) ReleaseToProduction();
        CompletedDate = DateTime.Now;
        Status = ManufacturingStatus.Completed;
    }

    public void Ship() {
        if (ReleasedDate is null) ReleaseToProduction();
        if (CompletedDate is null) Complete();
        ShippedDate = DateTime.Now;
        Status = ManufacturingStatus.Shipped;
    }

    public void Cancel() {
        Status = ManufacturingStatus.Canceled;
    }


}