namespace OrderManager.ApplicationCore.Domain;

public class Trigger {
    public TriggerType Type { get; set; }
    public Order? Order { get; set; }
}

public class TriggerConfiguration {

    public string Name { get; set; } = string.Empty;

    public TriggerType TriggerType { get; set; }

    public ActionType ActionType { get; set; }
    public int ActionId { get; set; }
    public ITriggerAction? Action { get; set; }
}

public enum ActionType {
    Email,
    Report,
    Script,
    Label
}

public enum TriggerType {
    OrderCreated = 1,
    OrderReleased = 2,
    OrderCompleted = 3,
    OrderShipped = 4
}

public interface ITriggerAction {
	public abstract void DoAction(TriggerType triggerType, object data);
}