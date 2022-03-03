using OrderManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace OrderManager.Features.ProductDesigner;

public class ProductDesignerViewModel : ViewModelBase {

    public ObservableCollection<ProductAttribute> Attributes { get; set; } = new();

    public ReactiveCommand<Unit, Unit> AddAttributeCommand { get; }

    public ProductDesignerViewModel() {
        AddAttributeCommand = ReactiveCommand.Create(AddAttribute);

        Attributes.CollectionChanged += (o, args) => {
            if (o is null) return;
            var collection = (o as ObservableCollection<ProductAttribute>);
            if (collection is null) return;
            o = collection.Where(a => a.Enabled);
        };

    }

    private void AddAttribute() {
        Attributes.Add(new() {
            Enabled = true
        }) ;
    }

}

public class ProductAttribute : ReactiveObject {

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


