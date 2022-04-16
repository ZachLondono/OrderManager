using OrderManager.ApplicationCore.Emails;
using OrderManager.ApplicationCore.Labels;
using OrderManager.ApplicationCore.Plugins;
using OrderManager.Domain.Emails;
using OrderManager.Domain.Labels;
using OrderManager.Domain.Orders;
using OrderManager.Domain.Profiles;
using PluginContracts.Interfaces;

namespace OrderManager.ApplicationCore.Profiles;

public class ReleaseService {

    private readonly EmailService _emailService;
    private readonly LabelService _labelService;
    private readonly IPluginManager _pluginManager;
    private readonly LabelQuery.GetLabelById _labelQuery;
    private readonly EmailQuery.GetEmailById _emailQuery;

    public ReleaseService(EmailService emailService, EmailQuery.GetEmailById emailQuery, LabelService labelService, LabelQuery.GetLabelById labelQuery, IPluginManager pluginManager) {
        _emailService = emailService;
        _labelService = labelService;
        _pluginManager = pluginManager;
        _labelQuery = labelQuery;
        _emailQuery = emailQuery;
    }

    public async Task Release(Order order, ReleaseProfile profile) {

        foreach (var labelId in profile.Labels) {
            LabelFieldMap? label = await _labelQuery(labelId);
            if (label is null) continue;
            await _labelService.PrintLabels(order, label);
        }

        
        foreach (var emailId in profile.Emails) {
            EmailTemplate? email = await _emailQuery(emailId);
            if (email is null) continue;
            // TODO: figure out something for sender
            await _emailService.SendEmail(order, email, "");
        }

        var releasePlugins = _pluginManager.GetPluginTypes()
                                    .Where(p => profile.Plugins.Contains(p.Name))
                                    .Where(p => p.StartUpType.GetInterface(nameof(IReleaseAction)) != null);

        if (releasePlugins.Any()) {
            foreach (var plugin in releasePlugins) {
                // TODO: create instance with dependency injection
                IReleaseAction? action = (IReleaseAction?)Activator.CreateInstance(plugin.StartUpType);
                if (action is null) continue;
                await action.Run(order);
            }
        }

    }

}
