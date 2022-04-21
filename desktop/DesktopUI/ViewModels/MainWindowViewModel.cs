using Avalonia.Controls;
using DesktopUI.Views;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class MainWindowViewModel : ViewModelBase {

    public string Greeting => "Welcome to Avalonia!";

    public MainWindowViewModel() {

        ShowDialog = new Interaction<DialogWindowContent, Unit>();

        EditLabelsCommand = ReactiveCommand.CreateFromTask(OpenLabelEditorDialog);
        EditEmailsCommand = ReactiveCommand.CreateFromTask(OpenEmailEditorDialog);
        EditProfilesCommand = ReactiveCommand.CreateFromTask(OpenProfileEditorDialog);

    }

    public Interaction<DialogWindowContent, Unit> ShowDialog { get; }

    public ICommand EditLabelsCommand { get; }
    public ICommand EditEmailsCommand { get; }
    public ICommand EditProfilesCommand { get; }

    private async Task OpenLabelEditorDialog() {
        await ShowDialog.Handle(new(450, 500, new LabelFieldEditorView {
            DataContext = App.GetRequiredService<LabelFieldEditorViewModel>()
        }));
    }
    
    private async Task OpenEmailEditorDialog() {
        await ShowDialog.Handle(new(450, 500, new EmailTemplateEditorView()));
    }

    private async Task OpenProfileEditorDialog() {
        await ShowDialog.Handle(new(450, 500, new ReleaseProfileEditorView()));
    }

    public record DialogWindowContent(int Width, int Height, IControl Content);

}