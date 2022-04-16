using OrderManager.Domain.Emails;
using OrderManager.Domain.Labels;

namespace OrderManager.Domain.Profiles;

public class ReleaseProfile {

    public int Id { get; init; }

    public string Name { get; private set; }

    private readonly List<int> _emails;
    public IReadOnlyCollection<int> Emails => _emails;

    private readonly List<int> _labels;
    public IReadOnlyCollection<int> Labels => _labels;

    private readonly List<string> _plugins;
    public IReadOnlyCollection<string> Plugins => _plugins;

    public ReleaseProfile(int id, string name) {
        Id = id;
        Name = name;
        _emails = new();
        _labels = new();
        _plugins = new();
    }

    public ReleaseProfile(int id, string name, IEnumerable<int> emails, IEnumerable<int> labels, IEnumerable<string> plugins) {
        Id = id;
        Name = name;
        _emails = new(emails);
        _labels = new(labels);
        _plugins = new(plugins);
    }

    public void SetName(string name) {
        Name = name;
    }

    public void AddEmailTemplate(int emailTemplateId) {
        _emails.Add(emailTemplateId);
    }

    public void RemoveEmailTemplate(int emailTemplateId) {
        _emails.Remove(emailTemplateId);
    }

    public void AddLabelFieldMap(int labelId) {
        _labels.Add(labelId);
    }

    public void RemoveLabelFieldMap(int labelId) {
        _labels.Remove(labelId);
    }

    public void AddPlugin(string pluginName) {
        _plugins.Add(pluginName);
    }

    public void RemovePlugin(string pluginName) {
        _plugins.Remove(pluginName);
    }

}
