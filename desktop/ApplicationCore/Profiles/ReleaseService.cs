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

    public Task Release(Order order, ReleaseProfile profile) {

        throw new NotImplementedException();

        // TODO: query label data and print labels
        /*foreach (var labelId in profile.Labels) {
            ...
        }*/

        // TODO: query email data and send emails
        /*foreach (var emailId in profile.Emails) {
            ...
        }*/

        // TODO: execute release profile plugins
        /*foreach (var pluginName in profile.Plugins) {
            ...
        }*/

    }

}
