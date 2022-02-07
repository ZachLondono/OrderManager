namespace OrderManager.ApplicationCore.Domain;

public class Priority {
    public int Id { get; set; }
    public string PriorityName { get; set; } = default!;
    public int Level { get; set; }
};
