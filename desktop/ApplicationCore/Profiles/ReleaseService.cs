using OrderManager.ApplicationCore.Emails;
using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Orders;
using OrderManager.Domain.Profiles;

namespace OrderManager.ApplicationCore.Profiles;

public class ReleaseService {

    private readonly EmailService _emailService;
    private readonly LabelService _labelService;

    public ReleaseService(EmailService emailService, LabelService labelService) {
        _emailService = emailService;
        _labelService = labelService;
    }

    public async Task Release(Order order, ReleaseProfile profile) {

        foreach (var label in profile.Labels) {
            await _labelService.PrintLabels(order, label);
        }

        foreach (var email in profile.Emails) {
            await _emailService.SendEmail(order, email, "");
        }

        // TODO: execute release profile plugins
        /*foreach (var plugin in profile.Plugins) {
            ...
        }*/

    }

}
