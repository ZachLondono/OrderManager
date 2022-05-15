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
        EditProfileCommand = ReactiveCommand.CreateFromTask<ReleaseProfileSummary>(OnEditProfile);
        DeleteProfileCommand = ReactiveCommand.CreateFromTask<ReleaseProfileSummary>(OnDeleteProfile);

        ShowDialog = new Interaction<ToolWindowContent, Unit>();

    }

    public Interaction<ToolWindowContent, Unit> ShowDialog { get; }

    public ICommand CreateProfileCommand { get; }
    public ICommand EditProfileCommand { get; }
    public ICommand DeleteProfileCommand { get; }

    private async Task OnCreateProfile() {

        var context = await _repo.Add("New Release Profile");

        var details = await OpenProfileEditor(context.Id);
        if (details is null) return;

        Profiles.Add(new() {
            Id = details.Id,
            Name = details.Name
        });

    }

    private async Task OnEditProfile(ReleaseProfileSummary profile) {

        var details = await OpenProfileEditor(profile.Id);
        if (details is null) return;

        int index = Profiles.IndexOf(profile);
        Profiles.Remove(profile);
        profile.Name = details.Name;
        Profiles.Insert(index, profile);

    }

    private async Task OnDeleteProfile(ReleaseProfileSummary profile) {
        await _repo.Remove(profile.Id);
        Profiles.Remove(profile);
    }

    public async Task LoadData() {
        Profiles.Clear();
        var profiles = await _summariesQuery();
        foreach (var profile in profiles) {
            Profiles.Add(profile);
        }
    }

    private async Task<ReleaseProfileDetails?> OpenProfileEditor(int profileId) {

        ReleaseProfileDetails? details = null;

        var editorvm = App.GetRequiredService<ReleaseProfileEditorViewModel>();

        await ShowDialog.Handle(new("Profile Editor", 450, 650, new ReleaseProfileEditorView {
            DataContext = editorvm
        }, async () => {

            details = await _detailsQuery(profileId);
            await editorvm.SetData(details);

        }));

        return details;

    }
}
