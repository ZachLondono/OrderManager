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

    public string Customer { get; set; }

    public DateTime? ScheduledDate { get; private set; } = null;

    public DateTime? ReleasedDate { get; private set; } = null;

    public DateTime? CompletedDate { get; private set; } = null;

    public DateTime? ShippedDate { get; private set; } = null;

    public ManufacturingStatus Status { get; private set; }

    private readonly List<ManufacturedProduct> _products;
    public IReadOnlyCollection<ManufacturedProduct> Products => Products;

    public Job(int id, string name, string number, string customer, IEnumerable<ManufacturedProduct> products) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Id = id;
        Name = name;
        Number = number;
        Customer = customer;
        _products = new(products);
        Status = ManufacturingStatus.Pending;
    }

    public Job(int id, string name, string number, string customer, IEnumerable<ManufacturedProduct> products, DateTime? scheduledDate, DateTime? releasedDate, DateTime? completedDate, DateTime? shippedDate, ManufacturingStatus status) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Id = id;
        Name = name;
        Number = number;
        Customer = customer;
        ScheduledDate = scheduledDate;
        ReleasedDate = releasedDate;
        CompletedDate = completedDate;
        ShippedDate = shippedDate;
        Status = status;
        _products = new(products);
    }

    public void Schedule(DateTime date) {
        if (date < DateTime.Today)
            throw new InvalidDataException("Date must be in the future");
        ScheduledDate = date;
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