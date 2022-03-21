using MediatR;
using Microsoft.Extensions.Logging;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unit = System.Reactive.Unit;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using Dapper;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class ProfileManagerViewModel : ViewModelBase {

    public ReleaseProfile? SelectedProfile { get; set; }

    public ReactiveCommand<Unit, Unit> CreateNewProfileCommands { get; }

    public ReactiveCommand<Unit, Unit> EditSelectedProfileCommand { get; }

    public ReactiveCommand<Unit, Unit> DeleteSelectedProfileCommand { get; }

    public ObservableCollection<ReleaseProfile> ReleaseProfiles { get; set; }

    private readonly ILogger<ProfileManagerViewModel> _logger;
    private readonly ISender _sender;

    public ProfileManagerViewModel(ILogger<ProfileManagerViewModel> logger, ISender sender) {

        _logger = logger;
        _sender = sender;

        CreateNewProfileCommands = ReactiveCommand.CreateFromTask(CreateNewProfile);

        EditSelectedProfileCommand = ReactiveCommand.CreateFromTask(EditSelectedProfile);

        DeleteSelectedProfileCommand = ReactiveCommand.CreateFromTask(DeleteSelectedProfile);

        var profiles = sender.Send(new GetReleaseProfiles.Query()).Result;

        ReleaseProfiles = new(profiles);

    }

    public async Task CreateNewProfile() {

        _logger.LogTrace("Create new profile command triggered");

        var logger = Program.GetService<ILogger<ProfileEditorViewModel>>();
        var sender = Program.GetService<ISender>();

        var profile = await _sender.Send(new CreateNewReleaseProfile.Command("Profile A"));

        var dialog = Program.CreateInstance<ProfileEditorDialog>();
        dialog.DataContext = new ProfileEditorViewModel(logger, sender, profile);

        var window = Program.GetService<MainWindow.MainWindow>();

        await dialog.ShowDialog(window);

        await UpdateProfileList();

    }

    public async Task EditSelectedProfile() {
        
        _logger.LogTrace("Edit selected profile command triggered");

        if (SelectedProfile is null) return;

        var logger = Program.GetService<ILogger<ProfileEditorViewModel>>();
        
        var profile = await _sender.Send(new GetReleaseProfileById.Query(SelectedProfile.Id));

        var dialog = Program.CreateInstance<ProfileEditorDialog>();
        dialog.DataContext = new ProfileEditorViewModel(logger, _sender, profile);

        var window = Program.GetService<MainWindow.MainWindow>();

        await dialog.ShowDialog(window);

        await UpdateProfileList();

    }

    public async Task DeleteSelectedProfile() {

        _logger.LogTrace("Delete selected profile command triggered");

        if (SelectedProfile is null) return;

        await _sender.Send(new DeleteReleaseProfile.Command(SelectedProfile.Id));

        await UpdateProfileList();

    }

    private async Task UpdateProfileList() {

        ReleaseProfiles.Clear();

        var profiles = await _sender.Send(new GetReleaseProfiles.Query());

        foreach (var profile in profiles) {
            ReleaseProfiles.Add(profile);
        }

    }

}

public class GetReleaseProfileById {

    public record Query(Guid Id) : IRequest<ReleaseProfileEventDomain>;

    public class Handler : IRequestHandler<Query, ReleaseProfileEventDomain> {

        private readonly ILogger<Handler> _logger;
        private readonly ReleaseProfileRepository _repository;

        public Handler(ILogger<Handler> logger, ReleaseProfileRepository repository) {
            _logger = logger;
            _repository =  repository;
        }

        public Task<ReleaseProfileEventDomain> Handle(Query request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling request for release profile");

            return Task.FromResult(_repository.GetProfileById(request.Id));

        }
    }

}

public class DeleteReleaseProfile {

    public record Command(Guid Id) : IRequest;

    public class Handler : IRequestHandler<Command> {
        
        private readonly ILogger<Handler> _logger;
        private readonly IDbConnection _connection;

        public Handler(ILogger<Handler> logger, IDbConnection connection) {
            _logger = logger;
            _connection = connection;
        }

        public Task<MediatR.Unit> Handle(Command request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling request for delete profile");

            const string command = "DELETE FROM [ReleaseProfiles] WHERE [Id] = @Id;";

            const string actionCommand = "DELETE FROM [ReleaseProfileActions] WHERE [ProfileId] = @Id;";

            var rows = _connection.Execute(command, request);

            rows += _connection.Execute(actionCommand, request);

            _logger.LogInformation("Release profiles deleted, id: {Id}, rows effected {rows}", request.Id, rows);

            return MediatR.Unit.Task;

        }
    }

}

public class GetReleaseProfiles {

    public record Query() : IRequest<IEnumerable<ReleaseProfile>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ReleaseProfile>> {

        private readonly ILogger<Handler> _logger;
        private readonly IDbConnection _connection;

        public Handler(ILogger<Handler> logger, IDbConnection connection) {
            _logger = logger;
            _connection = connection;
        }

        public Task<IEnumerable<ReleaseProfile>> Handle(Query request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling request for release profiles");

            const string query = "SELECT [Id], [Name] FROM [ReleaseProfiles];";

            var profiles = _connection.Query<ReleaseProfile>(query);

            return Task.FromResult(profiles);

        }
    }

}