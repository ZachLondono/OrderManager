using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Features.Scripts;

namespace OrderManager.ApplicationCore.Features.Triggers.TriggerActions;

public class ScriptAction : ITriggerAction {

    private readonly ScriptController _controller;

    public ScriptAction(ScriptController controller) {
        _controller = controller;
    }

    public void DoAction(TriggerType triggerType, object data) {
        throw new NotImplementedException();
    }
}