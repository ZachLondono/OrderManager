namespace OrderManager.Domain.Labels;

public class LabelFieldDoesNotExistExcpetion : Exception {

    public string LabelField { get; init; }

    public LabelFieldDoesNotExistExcpetion(string field)
        : base($"Label field '{field}' does not exist") {
        LabelField = field;
    }

}