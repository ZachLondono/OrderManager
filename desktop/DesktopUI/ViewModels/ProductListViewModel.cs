using DesktopUI.Common;
using DesktopUI.Views;
using OrderManager.ApplicationCore.Catalog;
using OrderManager.Domain.Catalog;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels;

public class ProductListViewModel : ViewModelBase {

    public ObservableCollection<ProductSummary> Products { get; } = new();

    private readonly ICatalogAPI _api;

    public ProductListViewModel(ICatalogAPI api) {
        _api = api;

        ShowDialog = new Interaction<ToolWindowContent, Unit>();

        EditProductCommand = ReactiveCommand.CreateFromTask<ProductSummary>(OnProductEdit);
        CreateProductCommand = ReactiveCommand.CreateFromTask(OnProductCreate);
        DeleteProductCommand = ReactiveCommand.CreateFromTask<ProductSummary>(OnProductDelete);

    }

    public Interaction<ToolWindowContent, Unit> ShowDialog { get; }

    public ICommand EditProductCommand { get; }
    public ICommand DeleteProductCommand { get; }
    public ICommand CreateProductCommand { get; }

    public async Task LoadData() {

        while (true) { 
            try { 

                var products = await _api.GetProducts();

                Products.Clear();
                foreach (var product in products) {
                    Products.Add(product);
                }

                break;

            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }
        }

    }
    
    private class AddedProductResult {
        public int Id { get; set; }
    }

    private async Task OnProductCreate() {
        
        try { 
            string newName = "New Product";
            var result = await _api.AddToCatalog(new() {
                Name = newName
            });

            Products.Add(new() {
                Id = (result.Value as AddedProductResult)?.Id ?? -1,
                Name = newName
            });

        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

    private async Task OnProductEdit(ProductSummary product) {

        Product? details = null;
        var editorvm = App.GetRequiredService<ProductDesignerViewModel>();

        await ShowDialog.Handle(new("Product Editor", 400, 500, new ProductDesignerView {
            DataContext = editorvm
        }, async () => {

            try { 
                details = await _api.GetProductDetails(product.Id);
                editorvm.SetData(details);
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }

        }));

        if (details is not null) { 
            var index = Products.IndexOf(product);
            Products.RemoveAt(index);
            product.Name = details.Name;
            Products.Insert(index, product);
        }

    }

    private async Task OnProductDelete(ProductSummary product) {

        try {

            await _api.RemoveFromCatalog(product.Id);

        } catch (Exception ex) {
            Debug.WriteLine(ex);
        }

    }

}
