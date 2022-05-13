namespace DesktopUI.Models;

public class ProductAttributeTracker {

    public bool HasChanged { get; private set; }

    public string PreviousName { get; private set; } = string.Empty;

    private string _name;
    public string Name {
        get => _name;
        set { 
            _name = value;
            HasChanged = true;
        }
    }
    
    public string? PreviousDefault { get; private set; } = string.Empty;

    private string? _default;
    public string? Default {
        get => _default;
        set {
            _default = value;
            HasChanged = true;
        }
    }

    public ProductAttributeTracker(string name, string? defaultValue) {
        PreviousName = name;
        _name = name;
        PreviousDefault = defaultValue;
        _default = defaultValue;
    }

    public void Reset() {
        HasChanged = false;
        PreviousName = Name;
        PreviousDefault = Default;
    }

}
