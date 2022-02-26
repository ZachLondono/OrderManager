using OrderManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;

namespace OrderManager.Features.ProductDesigner;

public class ProductDesignerViewModel : ViewModelBase {

    public ObservableCollection<ProductAttribute> Attributes { get; set; } = new();

    public ReactiveCommand<Unit, Unit> AddAttributeCommand { get; }

    public ProductDesignerViewModel() {
        AddAttributeCommand = ReactiveCommand.Create(AddAttribute);
    }

    private void AddAttribute() {
        Attributes.Add(new());
    }

}

public class ProductAttribute {

    public string Name { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

}


