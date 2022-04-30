using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Profiles;
using OrderManager.Domain.Profiles;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class ProfileListViewModel : ViewModelBase {

    public ObservableCollection<ReleaseProfileSummary> Profiles { get; } = new();

    private readonly IReleaseProfileRepository _repo;
    private readonly ProfileQuery.GetProfileSummaries _summariesQuery;
    private readonly ProfileQuery.GetProfileDetailsById _detailsQuery;

    public ProfileListViewModel(IReleaseProfileRepository repo, ProfileQuery.GetProfileSummaries summariesQuery, ProfileQuery.GetProfileDetailsById detailsQuery) {
        
        _repo = repo;
        _summariesQuery = summariesQuery;
        _detailsQuery = detailsQuery;

        CreateProfileCommand = ReactiveCommand.CreateFromTask(OnCreateProfile);
        EditProfileCommand = ReactiveCommand.CreateFromTask<ReleaseProfileSummary, Unit>(OnEditProfile);
        DeleteProfileCommand = ReactiveCommand.CreateFromTask<ReleaseProfileSummary, Unit>(OnDeleteProfile);

        ShowDialog = new Interaction<ToolWindowContent, Unit>();

    }

    public Interaction<ToolWindowContent, Unit> ShowDialog { get; }

    public ICommand CreateProfileCommand { get; }
    public ICommand EditProfileCommand { get; }
    public ICommand DeleteProfileCommand { get; }

    private async Task OnCreateProfile() {

        var context = await _repo.Add("New Release Profile");

        var details = await _detailsQuery(context.Id);

        var editorvm = App.GetRequiredService<ReleaseProfileEditorViewModel>();
        _ = editorvm.SetData(details);

        await ShowDialog.Handle(new("Profile Editor", 450, 650, new ReleaseProfileEditorView {
            DataContext = editorvm
        }));

        Profiles.Add(new() {
            Id = details.Id,
            Name = details.Name
        });

    }

    private async Task<Unit> OnEditProfile(ReleaseProfileSummary profile) {
        
        var details = await _detailsQuery(profile.Id);

        var editorvm = App.GetRequiredService<ReleaseProfileEditorViewModel>();
        _ = editorvm.SetData(details);

        await ShowDialog.Handle(new("Profile Editor", 450, 650, new ReleaseProfileEditorView {
            DataContext = editorvm
        }));

        int index = Profiles.IndexOf(profile);
        Profiles.Remove(profile);
        profile.Name = details.Name;
        Profiles.Insert(index, profile);

        return Unit.Default;

    }

    private async Task<Unit> OnDeleteProfile(ReleaseProfileSummary profile) {
        await _repo.Remove(profile.Id);
        Profiles.Remove(profile);
        return Unit.Default;
    }

    public async Task LoadData() {
        Profiles.Clear();
        var profiles = await _summariesQuery();
        foreach (var profile in profiles) {
            Profiles.Add(profile);
        }
    }

}
