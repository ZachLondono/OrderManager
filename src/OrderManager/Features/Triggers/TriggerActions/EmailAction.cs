using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Triggers.TriggerActions;

public class EmailAction : ITriggerAction {

    private readonly Email _email;

    public EmailAction(Email email) {
        _email = email;
    }

    public void DoAction(TriggerType triggerType, object data) {
        throw new NotImplementedException();
    }

}