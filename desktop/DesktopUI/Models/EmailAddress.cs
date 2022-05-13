namespace DesktopUI.Models;

/// <summary>
/// Keeps track of changes to an email field and keeps the previous value so that it can be overwritten when the new value is saved
/// </summary>
public class EmailAddress {

    public bool HasChanged { get; private set; }

    public string? PreviousValue {
        get;
        private set;
    } = string.Empty;

    private string _value;
    public string Value {
        get => _value;
        set {
            _value = value;
            HasChanged = true;
        }
    }

    public EmailAddress(string initialValue) {
        _value = initialValue;
        PreviousValue = initialValue;
    }

    public void Reset() {
        HasChanged = false;
        PreviousValue = Value;
    }

}