using MediatR;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Scripts;

public class ScriptController : BaseController {

    private readonly ScriptEngine _engine;
    private IEnumerable<Script>? _availableScripts;

    public ScriptController(ISender sender) : base(sender) {
        _engine = Python.CreateEngine();
    }

    public Task<Script> CreateScript(string name, string source) {
        return Sender.Send(new CreateScript.Command(name, source));
    }

    public Task<bool> DeleteScript(string name) {
        return Sender.Send(new DeleteScript.Command(name));
    }

    public async Task LoadScripts() {
        _availableScripts = await Sender.Send(new GetScripts.Query());
    }

    public async Task<IEnumerable<Script>> GetAvailableScripts() {
        if (_availableScripts is null) 
            await LoadScripts();
        return _availableScripts ?? Enumerable.Empty<Script>();
    }

    public async Task<ScriptResult> ExecuteScript(string name, dynamic data) {

        if (_availableScripts is null) await LoadScripts();

        Script? script = _availableScripts?
                                .Where(s => s.Name.Equals(name))
                                .FirstOrDefault();

        if (script is null) 
            return new ScriptResult(ScriptStatus.Failure, $"Script '{name}' Not Found");

        return await ExceuteScript(script, data);

    }

    public async Task<ScriptResult> ExceuteScript(Script script, dynamic data) {

        if (!File.Exists(script.Source)) {
            return new(ScriptStatus.Failure, $"'{script.Name}' script source does not exist '{script.Source}'");
        }

        return await Task.Run<ScriptResult>(() => {

            ScriptScope scope = _engine.ExecuteFile(script.Source);

            const string functionName = "Script";

            if (!scope.TryGetVariable(functionName, out dynamic func)) {
                return new(ScriptStatus.Failure, $"'{script.Name}' script entry point not found '{functionName}'");
            }

            try { 
                var result = func(data);
                return new(ScriptStatus.Success, result);
            } catch (Exception e) {
                return new(ScriptStatus.Failure, $"Script failed to execute\n{e}");
            }

        });

    }

}