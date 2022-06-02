using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace DesktopUI.Controls;
public partial class MonthlyPlanner : UserControl {

    public MonthlyPlanner() {
        InitializeComponent();

        /*var monthNameBlock = this.FindControl<TextBlock>("MonthName");
        if (monthNameBlock is not null)
            monthNameBlock.Text = Date.ToString("MMMM");*/

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}