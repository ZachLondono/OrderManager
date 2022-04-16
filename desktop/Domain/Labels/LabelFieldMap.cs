namespace OrderManager.Domain.Labels;

/// <summary>
/// Describes the type of data that the label uses
/// </summary>
public enum LabelType {

    /// <summary>
    /// Order labels get printed once per order and only contain order information
    /// </summary>
    Order,

    /// <summary>
    /// LineItem labels get printed for each item in the the order and can contain order information or information specific to the item
    /// </summary>
    LineItem

}

public class LabelFieldMap {

    public int Id { get; init; }

    public string Name { get; private set; }

    public string TemplatePath { get; init; }

    public int PrintQty { get; private set; }

    private readonly Dictionary<string, string> _fields;
    public IReadOnlyDictionary<string, string> Fields => _fields;

    public LabelType Type { get; private set; }

    public LabelFieldMap(int id, string name, string templatePath, int printQty, LabelType type, Dictionary<string, string> fields) {
        Id = id;
        Name = name;
        TemplatePath = templatePath;
        PrintQty = printQty;
        Type = type;
        _fields = fields;
    }

    public void SetName(string name) {
        Name = name;
    }

    public void SetPrintQty(int printQty) {
        PrintQty = printQty;
    }

    public void SetField(string field, string value) {
        if (!_fields.ContainsKey(field)) throw new LabelFieldDoesNotExistExcpetion(field);
        _fields[field] = value;
    }

    public void SetType(LabelType type) {
        Type = type;
    }

}