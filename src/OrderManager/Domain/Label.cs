namespace OrderManager.ApplicationCore.Domain;

public class Label {

    public int Id { get; set; }

    public string? Name { get; set; }

    public string? TemplatePath { get; set; }

    public LabelType Type { get; set; }

    public IEnumerable<LabelField>? Fields { get; set; }

}

public class LabelField {
    public string? Key { get; set; }
    public string? Value { get; set; }
}

public enum LabelType {
    Order,
    Product
}
