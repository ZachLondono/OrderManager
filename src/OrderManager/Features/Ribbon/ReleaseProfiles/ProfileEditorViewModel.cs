using MediatR;
using Microsoft.Extensions.Logging;
using OrderManager.Shared;
using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using Unit = System.Reactive.Unit;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using Domain.Services;
using PluginContracts.Interfaces;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class ProfileEditorViewModel : ViewModelBase {

    private string _profileName = "Profile A";
    public string ProfileName {
        get => _profileName;
        set {
            _profileName = value;
            _profile.ChangeProfileName(value);
        }
    }

    public string? LeftSelected { get; set; }

    public string? RightSelected { get; set; }

    public ObservableCollection<string> ProfileActions { get; }

    public ObservableCollection<string> AvailableActions { get; }

    public ReactiveCommand<Unit, Unit> SaveProfileCommand { get; } 

    public ReactiveCommand<Unit, Unit> MoveActionToProfileCommand { get; }

    public ReactiveCommand<Unit, Unit> RemoveActionFromProfileCommand { get; }

    private readonly ILogger<ProfileEditorViewModel> _logger;
    private readonly ISender _sender;
    private readonly ReleaseProfileEventDomain _profile;

    public ProfileEditorViewModel(ILogger<ProfileEditorViewModel> logger, ISender sender, ReleaseProfileEventDomain profile) {

        _logger = logger;
        _sender = sender;
        _profile = profile;

        SaveProfileCommand = ReactiveCommand.Create(SaveProfile);

        MoveActionToProfileCommand = ReactiveCommand.Create(MoveActionToProfile);

        RemoveActionFromProfileCommand = ReactiveCommand.Create(RemoveActionFromProfile);

        ProfileName = profile.ProfileName;

        ProfileActions = new(profile.GetActions());

        IEnumerable<string> allActions = sender.Send(new GetAvailableActions.Query()).Result;

        AvailableActions = new(allActions.Where(a => !ProfileActions.Contains(a)));

    }

    private void SaveProfile() {
        _logger.LogTrace("Save profile command triggered");
        if (_profile is null) return;
        _sender.Send(new UpdateReleaseProfile.Command(_profile));
    }

    private void MoveActionToProfile() {
        _logger.LogTrace("Add action command triggered");

        if (RightSelected is null) return;

        string actionName = RightSelected;

        _profile.AddAction(actionName);
        ProfileActions.Add(actionName);
        AvailableActions.Remove(actionName);
    }

    private void RemoveActionFromProfile() {
        _logger.LogTrace("Remove action command triggered");

        if (LeftSelected is null) return;

        string actionName = LeftSelected;

        _profile.RemoveAction(actionName);
        AvailableActions.Add(actionName);
        ProfileActions.Remove(actionName);
    }

}

public class GetAvailableActions {

    public record Query() : IRequest<IEnumerable<string>>;

    public class Handler : IRequestHandler<Query, IEnumerable<string>> {

        private readonly ILogger<Handler> _logger;

        private readonly IPluginService _pluginService;

        public Handler(ILogger<Handler> logger, IPluginService pluginService) {
            _logger = logger;
            _pluginService = pluginService;
        }

        public Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling query for available release actions");

            var types = _pluginService.GetPluginTypes<IReleaseAction>();

            List<string> availableActions = new();

            foreach (var type in types) {
                availableActions.Add(type.Name);
            }

            return Task.FromResult(availableActions.AsEnumerable());

        }

    }

}
