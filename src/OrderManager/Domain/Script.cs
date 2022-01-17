namespace OrderManager.ApplicationCore.Domain;

public class Script {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

}

public enum ScriptStatus {
    Success,
    Failure
}

public record ScriptResult(ScriptStatus Status, dynamic Result);