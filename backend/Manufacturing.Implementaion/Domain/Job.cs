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

    public int OrderId { get; init; }

    public string Name { get; private set; }

    public string Number { get; private set; }

    public string Customer { get; set; }

    public DateTime? ScheduledDate { get; private set; } = null;

    public DateTime? ReleasedDate { get; private set; } = null;

    public DateTime? CompletedDate { get; private set; } = null;

    public DateTime? ShippedDate { get; private set; } = null;

    public ManufacturingStatus Status { get; private set; }

    public int ProductClass { get; init; }

    public int ProductQty { get; init; }

    public int? WorkCell { get; set; } = null;

    public Job(int id, int orderId, string name, string number, string customer,int productClass, int productQty) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Id = id;
        OrderId = orderId;
        Name = name;
        Number = number;
        Customer = customer;
        Status = ManufacturingStatus.Pending;
        ProductClass = productClass;
        ProductQty = productQty;
        WorkCell = null;
    }

    public Job(int id, int orderId, string name, string number, string customer,
        DateTime? scheduledDate, DateTime? releasedDate, DateTime? completedDate, DateTime? shippedDate,
        ManufacturingStatus status, int productClass, int productQty, int? workCell) {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Name cannot be null or empty", nameof(number));

        Id = id;
        OrderId = orderId;
        Name = name;
        Number = number;
        Customer = customer;
        ScheduledDate = scheduledDate;
        ReleasedDate = releasedDate;
        CompletedDate = completedDate;
        ShippedDate = shippedDate;
        Status = status;
        ProductClass = productClass;
        ProductQty = productQty;
        WorkCell = workCell;

    }
    

    public void Complete() {
        CompletedDate = DateTime.Now;
        Status = ManufacturingStatus.Completed;
    }

    public void Ship() {
        if (CompletedDate is null) Complete();
        ShippedDate = DateTime.Now;
        Status = ManufacturingStatus.Shipped;
    }

    public void Cancel() {
        Status = ManufacturingStatus.Canceled;
    }

}