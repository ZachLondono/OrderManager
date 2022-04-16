using OrderManager.Domain.Emails;
using OrderManager.Domain.Labels;

namespace OrderManager.Domain.Profiles;

public class ReleaseProfile {

    public int Id { get; init; }

    public string Name { get; private set; }

    private readonly List<EmailTemplate> _emails;
    public IReadOnlyCollection<EmailTemplate> Emails => _emails;

    private readonly List<LabelFieldMap> _labels;
    public IReadOnlyCollection<LabelFieldMap> Labels => _labels;

    private readonly List<string> _plugins;
    public IReadOnlyCollection<string> Plugins => _plugins;

    public ReleaseProfile(int id, string name) {
        Id = id;
        Name = name;
        _emails = new();
        _labels = new();
        _plugins = new();
    }

    public ReleaseProfile(int id, string name, IEnumerable<EmailTemplate> emails, IEnumerable<LabelFieldMap> labels, IEnumerable<string> plugins) {
        Id = id;
        Name = name;
        _emails = new(emails);
        _labels = new(labels);
        _plugins = new(plugins);
    }

    public void SetName(string name) {
        Name = name;
    }

    public void AddEmailTemplate(EmailTemplate emailTemplate) {
        _emails.Add(emailTemplate);
    }

    public void RemoveEmailTemplate(EmailTemplate emailTemplate) {
        _emails.Remove(emailTemplate);
    }

    public void AddLabelFieldMap(LabelFieldMap label) {
        _labels.Add(label);
    }

    public void RemoveLabelFieldMap(LabelFieldMap label) {
        _labels.Remove(label);
    }

    public void AddPlugin(string pluginName) {
        _plugins.Add(pluginName);
    }

    public void RemovePlugin(string pluginName) {
        _plugins.Remove(pluginName);
    }

}
