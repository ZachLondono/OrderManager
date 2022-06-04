using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace DesktopUI.Views;
public partial class MonthlyPlannerView : UserControl {

    public MonthlyPlannerView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}