using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Triggers.TriggerActions;

public class LabelAction : ITriggerAction {

    private readonly Label _label;

    public LabelAction(Label label) {
        _label = label;
    }

    public void DoAction(TriggerType triggerType, object data) {
        throw new NotImplementedException();
    }
}
