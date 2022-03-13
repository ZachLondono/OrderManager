using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;

namespace OrderManager.Features.DeleteOrder;
public partial class DeleteOrderDialog : ReactiveWindow<DeleteOrderViewModel> {
    public DeleteOrderDialog() {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.WhenActivated(d => d(ViewModel!.DeleteOrderCommand.Subscribe(Close)));
        this.WhenActivated(d => d(ViewModel!.CancelDeleteCommand.Subscribe(Close)));

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
