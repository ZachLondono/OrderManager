using OrderManager.ApplicationCore.Profiles;
using OrderManager.Domain.Profiles;
using ReactiveUI;
using System;
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

    private bool _canSave = false;
    public bool CanSave {
        get => _canSave;
        set => this.RaiseAndSetIfChanged(ref _canSave, value);
    }

    private bool _nameChanged = false;

    private readonly IReleaseProfileRepository _repo;

    public ReleaseProfileEditorViewModel(IReleaseProfileRepository repo) {
        
        _repo = repo;

        SaveChangesCommand = ReactiveCommand.CreateFromTask(OnSaveChanges);

    }

    public ICommand SaveChangesCommand { get; }

    public void SetData(ReleaseProfileDetails profile) {
        Profile = profile;
    }

    public async Task OnSaveChanges() {

        if (_profile is null) return;

        var context = await _repo.GetById(_profile.Id);

        if (context is null) return;

        if (_nameChanged) context.SetName(_profile.Name);

        await _repo.Save(context);

    }

}
