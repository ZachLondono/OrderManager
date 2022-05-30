namespace Manufacturing.Contracts;

public class BackLog {

    public int Count { get; set; }

    public IEnumerable<BackLogItem> Jobs { get; set; } = Enumerable.Empty<BackLogItem>();

}
