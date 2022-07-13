using DesktopUI.Models;
using OrderManager.ApplicationCore.Catalog;
using OrderManager.Domain.Catalog;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            _nameChanged = true;
            CanSave = true;
        }
    }

    public ObservableCollection<ProductAttributeTracker> Attributes { get; set; } = new();

    private bool _canSave;
    public bool CanSave {
        get => _canSave;
        set => this.RaiseAndSetIfChanged(ref _canSave, value);
    }

    private bool _nameChanged;
    
    private readonly ICatalogController _api;

    public ProductDesignerViewModel(ICatalogController api) {
        _api = api;

        var canSave = this.WhenAny(x => x.CanSave, x => x.Value);
        SaveChangesCommand = ReactiveCommand.CreateFromTask(OnSaveChanges, canExecute: canSave);
        AddAttributeCommand = ReactiveCommand.CreateFromTask(OnAttributeAdded);
        RemoveAttributeCommand = ReactiveCommand.CreateFromTask<ProductAttributeTracker>(OnAttributeRemoved);
    }

    public ICommand SaveChangesCommand { get; }
    public ICommand AddAttributeCommand { get; }
    public ICommand RemoveAttributeCommand { get; }

    public void SetData(Product product) {
        _product = product;
        this.RaisePropertyChanged(nameof(Name));

        Attributes.Clear();
        foreach (var attr in _product.Attributes) {
            Attributes.Add(new(attr.Name, attr.Default));
        }

    }

    private async Task OnSaveChanges() {

        if (_product is null) return;
        
        try {

            if (_nameChanged) { 
                await _api.SetProductName(new() {
                    ProductId = _product.Id,
                    Name = _product.Name
                });
                _nameChanged = false;
            }

            foreach (var attribute in Attributes) {

                if (attribute is null || !attribute.HasChanged) continue;
                await _api.UpdateAttribute(new() {
                    ProductId = _product.Id,
                    OldAttribute = attribute.PreviousName,
                    NewAttribute = attribute.Name,
                    Default = attribute?.Default ?? ""
                });

                attribute!.Reset();

            }

            CanSave = false;

        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

    private async Task OnAttributeAdded() {

        if (_product is null) return;

        try {

            string newName = "New Attribute";

            int suffix = 1;
            while (Attributes.Any(a => a.PreviousName.Equals(newName) || a.Name.Equals(newName))) {
                newName = $"New Attribute {suffix++}";
            }

            await _api.AddAttribute(new() {
                ProductId = _product.Id,
                Name = newName,
                Default = ""
            });

            Attributes.Add(new(newName, ""));

        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

    private async Task OnAttributeRemoved(ProductAttributeTracker attribute) {

        if (_product is null) return;
        
        try {

            await _api.RemoveAttribute(new() {
                ProductId = _product.Id,
                Attribute = attribute.PreviousName
            });

            Attributes.Remove(attribute);

        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

}
