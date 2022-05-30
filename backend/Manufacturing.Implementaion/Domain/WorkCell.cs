namespace Manufacturing.Implementation.Domain;

public class WorkCell {

    public int Id { get; init; }

    public string Alias { get; private set; }

    public int ProductClass { get; init; }

    public int ExpectedMaxOutput { get; private set; }

    public List<ScheduledJob> Jobs { get; init; }

    public WorkCell(int id, string alias, int productClass, int expectedMaxOutput, IEnumerable<ScheduledJob> activeJobs) {
        Id = id;
        Alias = alias;
        ProductClass = productClass;
        ExpectedMaxOutput = expectedMaxOutput;
        Jobs = new(activeJobs);
    }

    public void SetAlias(string alias) {
        if (string.IsNullOrEmpty(alias) || string.IsNullOrWhiteSpace(alias))
            throw new ArgumentException("Alias cannot be empty", nameof(alias));

        Alias = alias;
    }

    public void SetExpectedMaxOutput(int expectedMaxOutput) {
        if (expectedMaxOutput < 0)
            throw new ArgumentOutOfRangeException(nameof(expectedMaxOutput), "Maximum expected output cannot be negative");

        ExpectedMaxOutput = expectedMaxOutput;
    }

}