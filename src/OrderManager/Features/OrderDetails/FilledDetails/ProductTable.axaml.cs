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

            var products = GetValue(ItemsOrderedProperty);
            if (products is null) return;

            // Map of the header to the column index
            Dictionary<string, int> headers = new();
            headers.Add("#", 0);
            headers.Add("Qty", 1);

            List<List<string>> rows = new();

            int i = 1;
            int colIdx = 2;
            foreach (var item in products) {

                var row = new List<string>();

                // Each row has a line number and quantity column, no matter the product type
                AddToRow(row,headers["#"],$"{i++}");
                AddToRow(row, headers["Qty"], $"{item.Qty}");

                // Then traverse through each item to find all of it's attributes
                foreach (var option in item.Options) {

                    if (!headers.ContainsKey(option.Key)) {
                        headers.Add(option.Key, colIdx++);
                    }
                    AddToRow(row, headers[option.Key], $"{option.Value}");
                }

                rows.Add(row);

            }

            var headerNames = headers.Keys.ToList();

            var indices = rows[0].Select((value, index) => index);
            foreach (var idx in indices) {
                _prodGrid.Columns.Add(new DataGridTextColumn { Header = $"{headerNames[idx]}", Binding = new Binding($"[{idx}]") });
            }

            _prodGrid.AutoGenerateColumns = false;
            _prodGrid.Items = rows;

        }
    }

    private void AddToRow(List<string> row, int col, string val) {

        while (row.Count - 1 < col) {
            row.Add(string.Empty);
        }

        row[col] = val;

    }
}
