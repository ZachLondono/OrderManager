using Avalonia.Controls;
using DesktopUI.Views;
using Infrastructure.Labels;
using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Labels;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {

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

        await ShowDialog.Handle(new(450, 600, new LabelFieldEditorView {
            DataContext = editorvm
        }));

    }
    private async Task OpenLabelListDialog() {
        await Task.Delay(1);
    }

    public record DialogWindowContent(int Width, int Height, IControl Content);

}