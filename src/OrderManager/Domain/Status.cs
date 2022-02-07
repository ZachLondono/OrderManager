namespace OrderManager.ApplicationCore.Domain;

public class Status {
    public int Id { get; set; }
    public string StatusName { get; set; } = default!;
    public int Level { get; set; }
}
