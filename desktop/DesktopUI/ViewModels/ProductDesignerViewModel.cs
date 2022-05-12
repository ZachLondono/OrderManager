using OrderManager.ApplicationCore.Catalog;
using OrderManager.Domain.Catalog;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class ProductDesignerViewModel : ViewModelBase {

    public string ErrorMessage { get; set; } = string.Empty;

    public bool HasError { get; set; }

    private Product? _product;

    public string Name {
        get => _product?.Name ?? "";
        set {
            if (_product is null) return;
            _product.Name = value;
        }
    }

    public ObservableCollection<ProductAttribute> Attributes { get; set; } = new();

    private readonly ICatalogAPI _api;

    public ProductDesignerViewModel(ICatalogAPI api) {
        _api = api;
        AddAttributeCommand = ReactiveCommand.Create(() => Debug.WriteLine("Added"));
        RemoveAttributeCommand = ReactiveCommand.Create(() => Debug.WriteLine("Removed"));
    }

    public ICommand AddAttributeCommand { get; }
    public ICommand RemoveAttributeCommand { get; }

    public void SetData(Product product) {
        _product = product;

        Attributes.Clear();
        foreach (var attr in _product.Attributes) {
            Attributes.Add(attr);
        }

    }

}
