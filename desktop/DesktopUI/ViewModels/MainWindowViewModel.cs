using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Labels;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {

    public MainWindowViewModel() {

        ShowDialog = new Interaction<DialogWindowContent, Unit>();
        ShowFileDialogAndReturnPath = new Interaction<Unit, string?>();

        CreateLabelsCommand = ReactiveCommand.CreateFromTask(OpenLabelCreatorDialog);
        ListLabelsCommand = ReactiveCommand.CreateFromTask(OpenLabelListDialog);

    }

    public Interaction<DialogWindowContent, Unit> ShowDialog { get; }
    public Interaction<Unit, string?> ShowFileDialogAndReturnPath { get; }

    public ICommand CreateLabelsCommand { get; }
    public ICommand ListLabelsCommand { get; }

    private async Task OpenLabelCreatorDialog() {

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

    }

    private async Task OpenLabelListDialog() {

        var query = App.GetRequiredService<LabelQuery.GetLabelSummaries>();
        var labels = await query();

        var listvm = App.GetRequiredService<LabelListViewModel>();
        listvm.Labels = new(labels);

        await ShowDialog.Handle(new("Label Templates", 450, 375, new LabelListView {
            DataContext = listvm
        }));

    }

}