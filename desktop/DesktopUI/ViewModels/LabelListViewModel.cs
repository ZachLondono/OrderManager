using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Labels;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace DesktopUI.ViewModels;

public class LabelListViewModel : ViewModelBase {

    public ObservableCollection<LabelFieldMapSummary> Labels { get; set; } = new();

    private readonly ILabelFieldMapRepository _repo;
    private readonly LabelQuery.GetLabelSummaries _query;
    private readonly LabelService _labelService;
    private readonly LabelQuery.GetLabelDetailsById _getLabelDetailsById;

    public LabelListViewModel(ILabelFieldMapRepository repo,
                                LabelQuery.GetLabelSummaries query,
                                LabelQuery.GetLabelDetailsById getLabelDetailsById,
                                LabelService labelService) {
        _repo = repo;
        _query = query;
        _getLabelDetailsById = getLabelDetailsById;
        _labelService = labelService;

        DeleteLabelCommand = ReactiveCommand.CreateFromTask<LabelFieldMapSummary>(OnDeleteLabel);
        EditLabelCommand = ReactiveCommand.CreateFromTask<LabelFieldMapSummary>(OnEditLabel);
        CreateLabelCommand = ReactiveCommand.CreateFromTask(OnCreateLabel);
        
        ShowDialog = new Interaction<ToolWindowContent, Unit>();
        ShowFileDialogAndReturnPath = new Interaction<Unit, string?>();
    }

    public Interaction<ToolWindowContent, Unit> ShowDialog { get; }
    public Interaction<Unit, string?> ShowFileDialogAndReturnPath { get; }

    public ReactiveCommand<LabelFieldMapSummary, Unit> DeleteLabelCommand { get; set; }
    public ReactiveCommand<LabelFieldMapSummary, Unit> EditLabelCommand { get; set; }
    public ReactiveCommand<Unit, Unit> CreateLabelCommand { get; set; }

    public async Task LoadData() {
        Labels.Clear();
        var labels = await _query();
        foreach (var label in labels) {
            Labels.Add(label);
        }
    }

    private async Task OnDeleteLabel(LabelFieldMapSummary label) {
        await _repo.Remove(label.Id);
        Labels.Remove(label);
    }

    private async Task OnEditLabel(LabelFieldMapSummary label) {

        var details = await OpenLabelEditor(label.Id);
        if (details is null) return;

        var index = Labels.IndexOf(label);
        Labels.RemoveAt(index);
        label.Name = details.Name;
        Labels.Insert(index, label);

    }

    private async Task OnCreateLabel() {

        string? path = await ShowFileDialogAndReturnPath.Handle(Unit.Default);

        if (path is null) return;
        
        var newContext = await _labelService.CreateLabelFieldMap(path);

        var details = await OpenLabelEditor(newContext.Id);
        if (details is null) return;

        Labels.Add(new() {
            Id = details.Id,
            Name = details.Name
        });

    }

    private async Task<LabelFieldMapDetails?> OpenLabelEditor(int labelId) {

        LabelFieldMapDetails? details = null;

        var editorvm = App.GetRequiredService<LabelFieldEditorViewModel>();

        await ShowDialog.Handle(new("Label Editor", 450, 600, new LabelFieldEditorView {
            DataContext = editorvm
        }, async () => {

            details = await _getLabelDetailsById(labelId);
            editorvm.SetData(details);

        }));

        return details;

    }

}
