using Microsoft.Extensions.Logging;
using OrderManager.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class ProfileManagerViewModel : ViewModelBase {

    public ReactiveCommand<Unit, Unit> CreateNewProfileCommands { get; }

    public ReactiveCommand<Unit, Unit> EditSelectedProfileCommand { get; }

    public ReactiveCommand<Unit, Unit> DeleteSelectedProfileCommand { get; }

    public IEnumerable<string> ReleaseProfiles { get; set; } = Enumerable.Empty<string>();

    public ProfileManagerViewModel(ILogger<ProfileManagerViewModel> logger) {

        CreateNewProfileCommands = ReactiveCommand.Create(() => logger.LogTrace("Create new profile command triggered"));

        EditSelectedProfileCommand = ReactiveCommand.Create(() => logger.LogTrace("Edit selected profile command triggered"));

        DeleteSelectedProfileCommand = ReactiveCommand.Create(() => logger.LogTrace("Delete selected profile command triggered"));

    }

}
