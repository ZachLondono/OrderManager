using OrderManager.Domain.Emails;
using OrderManager.Domain.Labels;
using OrderManager.Domain.Profiles;

namespace OrderManager.ApplicationCore.Profiles;

public record ProfileNameChangeEvent(string Name);
public record ProfileEmailAddedEvent(EmailTemplate Email);
public record ProfileEmailRemovedEvent(EmailTemplate Email);
public record ProfileLabelAddedEvent(LabelFieldMap Label);
public record ProfileLabelRemovedEvent(LabelFieldMap Label);
public record ProfilePluginAddedEvent(string PluginName);
public record ProfilePluginRemovedEvent(string PluginName);

public class ReleaseProfileContext {

    private readonly ReleaseProfile _profile;
    private readonly List<object> _events;

    public IReadOnlyCollection<object> Events => _events;

    internal ReleaseProfileContext(ReleaseProfile profile) {
        _profile = profile;
        _events = new List<object>();
    }

    public void SetName(string name) {
        _profile.SetName(name);
        _events.Add(new ProfileNameChangeEvent(name));
    }

    public void AddEmailTemplate(EmailTemplate email) {
        _profile.AddEmailTemplate(email);
        _events.Add(new ProfileEmailAddedEvent(email));
    }

    public void RemoveEmailTemplate(EmailTemplate email) {
        _profile.RemoveEmailTemplate(email);
        _events.Add(new ProfileEmailRemovedEvent(email));
    }

    public void AddLabelFieldMap(LabelFieldMap label) {
        _profile.AddLabelFieldMap(label);
        _events.Add(new ProfileLabelAddedEvent(label));
    }

    public void RemoveLabelFieldMap(LabelFieldMap label) {
        _profile.RemoveLabelFieldMap(label);
        _events.Add(new ProfileLabelRemovedEvent(label));
    }

    public void AddPlugin(string pluginName) {
        _profile.AddPlugin(pluginName);
        _events.Add(new ProfilePluginAddedEvent(pluginName));
    }

    public void RemovePlugin(string pluginName) {
        _profile.RemovePlugin(pluginName);
        _events.Add(new ProfilePluginRemovedEvent(pluginName));
    }
}