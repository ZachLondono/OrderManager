using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Domain.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace OrderManager.Features.OrderDetails.FilledDetails;
public partial class ProductTable : UserControl {

    private List<OrderedProduct>? _itemsOrdered;
    public List<OrderedProduct>? ItemsOrdered {
        get => _itemsOrdered;
        set => SetAndRaise(ItemsOrderedProperty, ref _itemsOrdered, value);
    }

    public static readonly StyledProperty<List<OrderedProduct>?> ItemsOrderedProperty = AvaloniaProperty
                                    .Register<ProductTable, List<OrderedProduct>?>(nameof(ItemsOrderedProperty));


    private string? _productName;
    public string? ProductName {
        get => _productName;
        set => SetAndRaise(ProductNameProperty, ref _productName, value);
    }

    public static readonly StyledProperty<string?> ProductNameProperty = AvaloniaProperty.Register<ProductTable, string?>(nameof(ProductNameProperty));

    /// <summary>
    /// The datagrid that holds all of the item information
    /// </summary>
    private DataGrid? _prodGrid;

    private TextBlock? _nameText;

    public ProductTable() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
        _prodGrid = this.FindControl<DataGrid>("ProductGrid");
        _nameText = this.FindControl<TextBlock>("ProductName");
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
        base.OnPropertyChanged(change);

        if (change.Property.Name == nameof(ProductNameProperty) && _nameText is not null) {
            _nameText.Text = GetValue(ProductNameProperty);
        } else if (change.Property.Name == nameof(ItemsOrderedProperty) && _prodGrid is not null) {

            // Builds a DataGrid which has all the product's options as columns and each line item in a row

            var items = GetValue(ItemsOrderedProperty);
            if (items is null) return;

            var products = items.Select(p => new OrderedProductEventDomain(p));

            Dictionary<string, int> headers = new();
            headers.Add("#", 0);
            headers.Add("Qty", 1);

            int colIdx = 2;
            foreach (var item in products)
                foreach (var option in item.Options)
                    if (!headers.ContainsKey(option.Key))
                        headers.Add(option.Key, colIdx++);


            var headerNames = headers.Keys.ToList();
            _prodGrid.AutoGenerateColumns = false;

            foreach (var header in headerNames) {

                var binding = new Binding();

                switch (header) {
                    case "Qty":
                        binding.Path = "Qty";
                        break;
                    // TODO: store line number in product
                    case "#":
                        binding.Path = "LineNumber";
                        break;
                   default:
                        binding.Path = $"[{header}]";
                        break;
                }

                
                _prodGrid.Columns.Add(new DataGridTextColumn {
                    Header = $"{header}",
                    Binding = binding
                });
            }
           
            _prodGrid.Items = products;

        }
    }

}
