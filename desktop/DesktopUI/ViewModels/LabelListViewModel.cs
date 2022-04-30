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

    public LabelListViewModel(ILabelFieldMapRepository repo, LabelQuery.GetLabelSummaries query) {
        _repo = repo;
        _query = query;

        DeleteLabelCommand = ReactiveCommand.CreateFromTask<LabelFieldMapSummary, Unit>(OnDeleteLabel);
        EditLabelCommand = ReactiveCommand.CreateFromTask<LabelFieldMapSummary, Unit>(OnEditLabel);
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

    private async Task<Unit> OnDeleteLabel(LabelFieldMapSummary label) {
        await _repo.Remove(label.Id);
        Labels.Remove(label);
        return Unit.Default;
    }

    private async Task<Unit> OnEditLabel(LabelFieldMapSummary label) {

        var query = App.GetRequiredService<LabelQuery.GetLabelDetailsById>();
        var details = await query(label.Id);

        var editorvm = App.GetRequiredService<LabelFieldEditorViewModel>();
        editorvm.SetData(details);

        await ShowDialog.Handle(new("Label Editor", 450, 600, new LabelFieldEditorView {
            DataContext = editorvm
        }));

        var index = Labels.IndexOf(label);
        Labels.RemoveAt(index);
        label.Name = details.Name;
        Labels.Insert(index, label);

        return Unit.Default;
    }

    private async Task OnCreateLabel() {

        string? path = await ShowFileDialogAndReturnPath.Handle(Unit.Default);

        if (path is null) return;

        var labelService = App.GetRequiredService<LabelService>();
        var newContext = await labelService.CreateLabelFieldMap(path);

        var query = App.GetRequiredService<LabelQuery.GetLabelDetailsById>();
        var details = await query(newContext.Id);

        var editorvm = App.GetRequiredService<LabelFieldEditorViewModel>();
        editorvm.SetData(details);

        await ShowDialog.Handle(new("Label Editor", 450, 600, new LabelFieldEditorView {
            DataContext = editorvm
        }));

        Labels.Add(new() {
            Id = details.Id,
            Name = details.Name
        });

    }

}
