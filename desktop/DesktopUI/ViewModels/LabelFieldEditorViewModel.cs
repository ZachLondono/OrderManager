using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Labels;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;
using static OrderManager.ApplicationCore.Labels.LabelQuery;

namespace DesktopUI.ViewModels;

public class LabelFieldEditorViewModel : ViewModelBase {

    private LabelFieldMapDetails _label;
    public LabelFieldMapDetails Label {
        get => _label;
        private set => this.RaiseAndSetIfChanged(ref _label, value);
    }

    private LabelFieldMapContext? _context;

    private readonly GetLabelDetailsById _queryById;
    private readonly ILabelFieldMapRepository _repo;

    public LabelFieldEditorViewModel(ILabelFieldMapRepository repo, GetLabelDetailsById queryById) {
        _repo = repo;
        _queryById = queryById;

        _label = new LabelFieldMapDetails {
            Id = 0,
            Name = string.Empty,
            PrintQty = 0,
            TemplatePath = string.Empty,
            Type = LabelType.Order,
            Fields = new()
        };

        SaveChangesCommand = ReactiveCommand.CreateFromTask(Save);
    }

    public ICommand SaveChangesCommand { get; }

    public async Task LoadLabel(int labelId) {
        Label = await _queryById(labelId);
        _context = await _repo.GetById(_label.Id);
    }

    public async Task Save() {
        if (_context is null) return;
        await _repo.Save(_context);
    }

    public void SetName(string name) {
        if (_context is null) return;
        _context.SetName(name);
    }

    public void SetPrintQty(int qty) {
        if (_context is null) return;
        _context.SetPrintQty(qty);
    }

    public void SetLabelType(LabelType type) {
        if (_context is null) return;
        _context.SetType(type);
    }

    public void SetFieldFormula(string field, string formula) {
        if (_context is null) return;
        _context.SetFieldFormula(field, formula);
    }

}