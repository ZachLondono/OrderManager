using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Features.Reports;

namespace OrderManager.ApplicationCore.Features.Triggers.TriggerActions;

public class ReportAction : ITriggerAction {

    private readonly ReportController _controller;

    public ReportAction(ReportController controller) {
        _controller = controller;
    }

    public void DoAction(TriggerType triggerType, object data) {
        throw new NotImplementedException();
    }
}