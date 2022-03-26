namespace Manufacturing.Implementation.Domain;

public enum ManufacturingStatus {
    Pending,
    InProgress,
    Complete,
    Shipped,
    Canceled
}

public class Job {

    public string Name { get; private set; }

    public string Number { get; private set; }

    public string? Customer { get; set; }

    public string? Vendor { get; set; }

    public int ItemCount { get; private set; }

    public DateTime? ReleaseDate { get; private set; } = null;

    public DateTime? CompleteDate { get; private set; } = null;

    public DateTime? ShippedDate { get; private set; } = null;

    public ManufacturingStatus Status { get; private set; }

    public Job(string name, string number, string? customer, string? vendor, int itemCount) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Name = name;
        Number = number;
        Customer = customer;
        Vendor = vendor;
        ItemCount = itemCount;
        Status = ManufacturingStatus.Pending;
    }

    public Job(string name, string number, string? customer, string? vendor, int itemCount, DateTime? releaseDate, DateTime? completeDate, DateTime? shippedDate, ManufacturingStatus status) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Name = name;
        Number = number;
        Customer = customer;
        Vendor = vendor;
        ItemCount = itemCount;
        ReleaseDate = releaseDate;
        CompleteDate = completeDate;
        ShippedDate = shippedDate;
        Status = status;
    }

    public void ReleaseToProduction() {
        ReleaseDate = DateTime.Now;
        Status = ManufacturingStatus.InProgress;
    }

    public void Complete() {
        if (ReleaseDate is null) ReleaseToProduction();
        CompleteDate = DateTime.Now;
        Status = ManufacturingStatus.Complete;
    }

    public void Ship() {
        if (ReleaseDate is null) ReleaseToProduction();
        if (CompleteDate is null) Complete();
        ShippedDate = DateTime.Now;
        Status = ManufacturingStatus.Shipped;
    }


}