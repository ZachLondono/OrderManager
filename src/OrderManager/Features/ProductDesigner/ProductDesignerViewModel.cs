using OrderManager.Shared;
using System;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ReactiveUI.Validation;
using System.Reactive.Linq;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;

namespace OrderManager.Features.ProductDesigner;

public class ProductDesignerViewModel : ViewModelBase, IValidatableViewModel {

    public IObservable<bool> ContainsDuplicates { get; }

    private bool _hasError = false;
    public bool HasError {
        get => _hasError;
        private set => this.RaiseAndSetIfChanged(ref _hasError, value);
    }

    private string _errorMessage = "Error Found";
    public string ErrorMessage {
        get => _errorMessage;
        private set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public ObservableCollection<ProductAttribute> Attributes { get; set; } = new();

    public ReactiveCommand<Unit, Unit> AddAttributeCommand { get; }

    public ValidationContext ValidationContext { get; } = new ValidationContext();

    public ProductDesignerViewModel() {
        
        AddAttributeCommand = ReactiveCommand.Create(AddAttribute);

        this.ValidationRule(x => x.Attributes,
                            attr => attr.Count > 1,
                            "Cannot contain duplicates");

        Attributes.CollectionChanged += (o, args) => {
            this.RaisePropertyChanged(nameof(Attributes));
            if (o is null) return;
            var collection = (o as ObservableCollection<ProductAttribute>);
            if (collection is null) return;
            o = collection.Where(a => a.Enabled);
        };

    }

    private void SetError() {
        HasError = true;
        ErrorMessage = "Duplicate Attributes not Allowed";
    }

    private void AddAttribute() {
        Attributes.Add(new() {
            Enabled = true
        }) ;
    }

}

public class ProductAttribute : ReactiveObject {

    private string _name;
    public string Name { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    private bool _enabled = true;
    public bool Enabled {
        get => _enabled;
        set => this.RaiseAndSetIfChanged(ref _enabled, value);
    }

    public ReactiveCommand<Unit, Unit> RemoveAttributeCommand { get; }

    public ProductAttribute() {
        RemoveAttributeCommand = ReactiveCommand.Create(RemoveAttribute);
    }

    private void RemoveAttribute() {
        Enabled = false;
    }

}