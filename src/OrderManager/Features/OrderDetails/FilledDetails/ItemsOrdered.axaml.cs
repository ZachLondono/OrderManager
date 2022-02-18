using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Domain.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace OrderManager.Features.OrderDetails.FilledDetails;
public partial class ItemsOrdered : UserControl {

    private List<OrderItem>? _productName;
    public List<OrderItem>? ProductName {
        get => _productName;
        set {
            Debug.WriteLine($"Setting ProductName: {value}");
            SetAndRaise(ProductNameProperty, ref _productName, value);
        }
    }

    public static readonly StyledProperty<List<OrderItem>?> ProductNameProperty = AvaloniaProperty
                                    .Register<ItemsOrdered, List<OrderItem>?>(nameof(ProductNameProperty));

    private DataGrid? _prodGrid;

    public ItemsOrdered() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
        _prodGrid = this.FindControl<DataGrid>("ProductGrid");
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
        base.OnPropertyChanged(change);

        if (change.Property.Name == nameof(ProductNameProperty) && _prodGrid is not null) {

            // Builds a DataGrid which has all the product's options as columns and each line item in a row

            var products = GetValue(ProductNameProperty);
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
                AddToRow(row,headers["#"],$"line: {i++}");
                AddToRow(row, headers["Qty"], $"{item.Qty.Value}");

                // Then traverse through each item to find all of it's attributes
                foreach (string key in item.OrderedItem.Options.Keys) {
                    if (!headers.ContainsKey(key)) {
                        headers.Add(key, colIdx++);
                    }
                    AddToRow(row, headers[key], $"{item.OrderedItem.Options[key]}");
                }

                rows.Add(row);

            }

            var headerNames = headers.Keys.ToList();

            foreach (var idx in rows.Select((value, index) => index)) {
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
