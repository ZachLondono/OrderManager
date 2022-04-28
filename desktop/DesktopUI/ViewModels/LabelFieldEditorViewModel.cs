using Avalonia.Data;
using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Labels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class LabelFieldEditorViewModel : ViewModelBase {

    public static IEnumerable<LabelType> LabelTypes => Enum.GetValues(typeof(LabelType)).Cast<LabelType>();

    private LabelFieldMapDetails? _label;
    public LabelFieldMapDetails? Label {
        get => _label;
        private set => this.RaiseAndSetIfChanged(ref _label, value);
    }

    public string LabelPath {
        get {
            var str = _label?.TemplatePath ?? "";
            if (str.Length > 80) str = string.Concat(str.AsSpan(0, 80), "...");
            return str;
        }
    }

    public string LabelName {
        get => _label?.Name ?? "";
        set {
            if (_label is null) return;
            CanSave = true;
            _nameChanged = true;
            _label.Name = value;
        }
    }

    public LabelType LabelType {
        get => _label?.Type ?? LabelType.Order;
        set {
            if (_label is null) return;
            CanSave = true;
            _typeChanged = true;
            _label.Type = value;
        }
    }

    public int LabelPrintQty {
        get => _label?.PrintQty ?? 0;
        set {
            if (_label is null) return;
            if (value < 0) throw new DataValidationException("Value must be positive");
            CanSave = true;
            _qtyChanged = true;
            _label.PrintQty = value;
        }
    }

    private Dictionary<string, LabelFieldFormula> _fields = new();
    public Dictionary<string, LabelFieldFormula> Fields {
        get => _fields;
        set => this.RaiseAndSetIfChanged(ref _fields, value);
    }

    private readonly ILabelFieldMapRepository _repo;

    // TODO: when a field is changed save it in a list of changes in the view model, then when the save button is pressed load the context apply the changes and then save
    private bool _canSave = false;
    public bool CanSave {
        get => _canSave;
        set => this.RaiseAndSetIfChanged(ref _canSave, value);
    }

    private bool _nameChanged = false;
    private bool _typeChanged = false;
    private bool _qtyChanged = false;

    public LabelFieldEditorViewModel(ILabelFieldMapRepository repo) {
        _repo = repo;

        var canSave = this.WhenAny(x => x.CanSave, x => x.Value);
        this.WhenAny(x => x._fields, x => x.Value.Select(a => a.Value.HasChanged));
        
        SaveChangesCommand = ReactiveCommand.CreateFromTask(Save, canExecute: canSave);
        LinkClicked = ReactiveCommand.Create(OpenFileLink);
    }

    public void SetData(LabelFieldMapDetails label) {
        _label = label;
        Fields = new();
        foreach (var kv in _label.Fields) {
            Fields.Add(kv.Key, new(kv.Value));
        }
    }

    public ICommand SaveChangesCommand { get; }
    public ICommand LinkClicked { get; }

    public async Task Save() {

        if (_label is null) return;

        LabelFieldMapContext context = await _repo.GetById(_label.Id);

        if (_nameChanged) context.SetName(_label.Name);
        if (_qtyChanged) context.SetPrintQty(_label.PrintQty);
        if (_typeChanged) context.SetType(_label.Type);
        
        foreach (var field in _fields) {
            if (field.Value.HasChanged) { 
                context.SetFieldFormula(field.Key, field.Value.Formula);
            }
        }

        await _repo.Save(context);

        _nameChanged = false;
        _typeChanged = false;
        _qtyChanged = false;
        foreach (var field in _fields) {
            field.Value.HasChanged = false;
        }
        CanSave = false;

    }

    public void OpenFileLink() {
        if (_label is null) return;
        try {
            new Process {
                StartInfo = new(_label.TemplatePath) {
                    UseShellExecute = true
                }
            }.Start();
        } catch (Exception e) {
            Debug.WriteLine(e);
        }
    }

    public class LabelFieldFormula {

        public bool HasChanged { get; set; }

        private string _formula;
        public string Formula {
            get => _formula;
            set {
                HasChanged = true;
                _formula = value;
            }
        }

        public LabelFieldFormula(string initialValue) => _formula = initialValue;

    }

}