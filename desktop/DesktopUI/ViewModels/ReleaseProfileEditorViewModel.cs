using OrderManager.ApplicationCore.Emails;
using OrderManager.ApplicationCore.Labels;
using OrderManager.ApplicationCore.Plugins;
using OrderManager.ApplicationCore.Profiles;
using OrderManager.Domain.Emails;
using OrderManager.Domain.Labels;
using OrderManager.Domain.Profiles;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class ReleaseProfileEditorViewModel : ViewModelBase {

    private ReleaseProfileDetails? _profile;
    public ReleaseProfileDetails? Profile {
        get => _profile;
        set => this.RaiseAndSetIfChanged(ref _profile, value);
    }

    public string Name {
        get => _profile?.Name ?? "";
        set {
            if (_profile is null) return;
            CanSave = true;
            _nameChanged = true;
            _profile.Name = value;
        }
    }

    private LabelFieldMapSummary? selectedLabelLeft;
    private LabelFieldMapSummary? selectedLabelRight;
    private EmailTemplateSummary? selectedEmailLeft;
    private EmailTemplateSummary? selectedEmailRight;
    private string? selectedPluginLeft;
    private string? selectedPluginRight;

    private bool _canSave = false;
    public bool CanSave {
        get => _canSave;
        set => this.RaiseAndSetIfChanged(ref _canSave, value);
    }

    private bool _nameChanged = false;
    
    record MoveAction<T>(T Moved, bool WasAdded);

    private readonly Queue<MoveAction<int>> labelMoves = new();
    private readonly Queue<MoveAction<int>> emailMoves = new();
    private readonly Queue<MoveAction<string>> pluginMoves = new();

    public ObservableCollection<LabelFieldMapSummary> SelectedLabels { get; } = new();
    public ObservableCollection<LabelFieldMapSummary> UnselectedLabels { get; } = new();
    public ObservableCollection<EmailTemplateSummary> SelectedEmails { get; } = new();
    public ObservableCollection<EmailTemplateSummary> UnselectedEmails { get; } = new();
    public ObservableCollection<string> SelectedPlugins { get; } = new();
    public ObservableCollection<string> UnselectedPlugins { get; } = new();

    private readonly IReleaseProfileRepository _repo;
    private readonly LabelQuery.GetLabelSummaries _getLabelSummaries;
    private readonly EmailQuery.GetEmailSummaries _getEmailSummaries;
    private readonly IPluginManager _pluginManager;

    public ReleaseProfileEditorViewModel(IReleaseProfileRepository repo, LabelQuery.GetLabelSummaries getLabelSummaries, EmailQuery.GetEmailSummaries getEmailSummaries, IPluginManager pluginManager) {
        
        _repo = repo;
        _getLabelSummaries = getLabelSummaries;
        _getEmailSummaries = getEmailSummaries;
        _pluginManager = pluginManager;

        SaveChangesCommand = ReactiveCommand.CreateFromTask(OnSaveChanges);
        SelectLabel = ReactiveCommand.Create(OnSelectLabel);
        DeselectLabel = ReactiveCommand.Create(OnDeselectLabel);
        SelectEmail = ReactiveCommand.Create(OnSelectEmail);
        DeselectEmail = ReactiveCommand.Create(OnDeselectEmail);
        SelectPlugin = ReactiveCommand.Create(OnSelectPlugin);
        DeselectPlugin = ReactiveCommand.Create(OnDeselectPlugin);

        LeftLabelClicked = ReactiveCommand.Create<LabelFieldMapSummary>((s) => selectedLabelLeft = s);
        RightLabelClicked = ReactiveCommand.Create<LabelFieldMapSummary>((s) => selectedLabelRight = s);
        LeftEmailClicked = ReactiveCommand.Create<EmailTemplateSummary>((s) => selectedEmailLeft = s);
        RightEmailClicked = ReactiveCommand.Create<EmailTemplateSummary>((s) => selectedEmailRight = s);
        LeftPluginClicked = ReactiveCommand.Create<string>((s) => selectedPluginLeft = s);
        RightPluginClicked = ReactiveCommand.Create<string>((s) => selectedPluginRight = s);

    }

    public ICommand SaveChangesCommand { get; }
    public ICommand SelectLabel { get; }
    public ICommand DeselectLabel { get; }
    public ICommand LeftLabelClicked { get; }
    public ICommand RightLabelClicked { get; }

    public ICommand SelectEmail { get; }
    public ICommand DeselectEmail { get; }
    public ICommand LeftEmailClicked { get; }
    public ICommand RightEmailClicked { get; }

    public ICommand SelectPlugin { get; }
    public ICommand DeselectPlugin { get; }
    public ICommand LeftPluginClicked { get; }
    public ICommand RightPluginClicked { get; }

    public async Task SetData(ReleaseProfileDetails profile) {
        Profile = profile;

        var labels = await _getLabelSummaries();
        var emails = await _getEmailSummaries();
        var plugins = _pluginManager.GetPluginTypes();

        foreach (var label in labels) {
            if (profile.LabelIds.Contains(label.Id)) {
                SelectedLabels.Add(label);
            } else {
                UnselectedLabels.Add(label);
            }
        }

        foreach (var email in emails) {
            if (profile.EmailIds.Contains(email.Id)) {
                SelectedEmails.Add(email);
            } else {
                UnselectedEmails.Add(email);
            }
        }

        foreach (var plugin in plugins) {
            if (profile.PluginNames.Contains(plugin.Name)) {
                SelectedPlugins.Add(plugin.Name);
            } else {
                UnselectedPlugins.Add(plugin.Name);
            }
        }

    }

    private void OnSelectLabel() {
        if (_profile is null || selectedLabelLeft is null) return;
        _profile.LabelIds.Add(selectedLabelLeft.Id);
        UnselectedLabels.Remove(selectedLabelLeft);
        SelectedLabels.Add(selectedLabelLeft);
        labelMoves.Enqueue(new(selectedLabelLeft.Id, true));
        CanSave = true;
        selectedLabelLeft = null;
    }

    private void OnDeselectLabel() {
        if (_profile is null || selectedLabelRight is null) return;
        _profile.LabelIds.Remove(selectedLabelRight.Id);
        SelectedLabels.Remove(selectedLabelRight);
        UnselectedLabels.Add(selectedLabelRight);
        labelMoves.Enqueue(new(selectedLabelRight.Id, false));
        CanSave = true;
        selectedLabelRight = null;
    }

    private void OnSelectEmail() {
        if (_profile is null || selectedEmailLeft is null) return;
        _profile.EmailIds.Add(selectedEmailLeft.Id);
        UnselectedEmails.Remove(selectedEmailLeft);
        SelectedEmails.Add(selectedEmailLeft);
        emailMoves.Enqueue(new(selectedEmailLeft.Id, true));
        CanSave = true;
        selectedEmailLeft = null;
    }

    private void OnDeselectEmail() {
        if (_profile is null || selectedEmailRight is null) return;
        _profile.EmailIds.Remove(selectedEmailRight.Id);
        SelectedEmails.Remove(selectedEmailRight);
        UnselectedEmails.Add(selectedEmailRight);
        emailMoves.Enqueue(new(selectedEmailRight.Id, false));
        CanSave = true;
        selectedEmailRight = null;
    }

    private void OnSelectPlugin() {
        if (_profile is null || selectedPluginLeft is null) return;
        _profile.PluginNames.Add(selectedPluginLeft);
        UnselectedPlugins.Remove(selectedPluginLeft);
        SelectedPlugins.Add(selectedPluginLeft);
        pluginMoves.Enqueue(new(selectedPluginLeft, true));
        CanSave = true;
        selectedPluginLeft = null;
    }

    private void OnDeselectPlugin() {
        if (_profile is null || selectedPluginRight is null) return;
        _profile.PluginNames.Remove(selectedPluginRight);
        SelectedPlugins.Remove(selectedPluginRight);
        UnselectedPlugins.Add(selectedPluginRight);
        pluginMoves.Enqueue(new(selectedPluginRight, false));
        CanSave = true;
        selectedPluginRight = null;
    }

    public async Task OnSaveChanges() {

        if (_profile is null) return;

        var context = await _repo.GetById(_profile.Id);

        if (context is null) return;

        if (_nameChanged) context.SetName(_profile.Name);

        static void ApplyMoves<TIn>(Queue<MoveAction<TIn>> moves, Action<TIn> addAction, Action<TIn> removeAction) {
            while (moves.Count > 0) {
                var move = moves.Dequeue();
                if (move.WasAdded)
                    addAction(move.Moved);
                else removeAction(move.Moved);
            }
        }

        ApplyMoves(labelMoves, context.AddLabelFieldMap, context.RemoveLabelFieldMap);
        ApplyMoves(emailMoves, context.AddEmailTemplate, context.RemoveEmailTemplate);
        ApplyMoves(pluginMoves, context.AddPlugin, context.RemovePlugin);

        await _repo.Save(context);

    }

}
