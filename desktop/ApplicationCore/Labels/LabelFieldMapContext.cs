using OrderManager.Domain.Labels;

namespace OrderManager.ApplicationCore.Labels;

public record LabelNameChangeEvent(string Name);
public record LabelTypeChangeEvent(LabelType Type);
public record LabelFieldFormulaSetEvent(string Field, string Formula);

public class LabelFieldMapContext {

    private readonly LabelFieldMap _map;

    private readonly List<object> _events = new();
    public IEnumerable<object> Events => _events;

    internal LabelFieldMapContext(LabelFieldMap map) {
        _map = map;
    }

    public void SetName(string name) {
        _map.SetName(name);
        _events.Add(new LabelNameChangeEvent(name));
    }

    public void SetType(LabelType type) {
        _map.SetType(type);
        _events.Add(new LabelTypeChangeEvent(type));
    }

    public void SetFieldFormula(string field, string formula) {
        _map.SetField(field, formula);
        _events.Add(new LabelFieldFormulaSetEvent(field, formula));
    }

}