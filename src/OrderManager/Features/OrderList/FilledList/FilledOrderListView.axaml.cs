using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace OrderManager.Features.OrderList.FilledList;
public partial class FilledOrderListView : UserControl {
    
    public FilledOrderListView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}
