using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace DesktopUI.Views;
public partial class ToolWindow : Window {

    public ToolWindow(int width, int height) {

        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.FindControl<Button>("PART_CloseButton").Click += delegate {
            Close();
        };

        var titleBar = this.FindControl<Control>("PART_TitleBar");
        titleBar.PointerPressed += (object? sender, PointerPressedEventArgs ep) => {
            PlatformImpl?.BeginMoveDrag(ep);
        };

        Width = width;
        Height = height + titleBar.Height;

    }

    public ToolWindow() : this(500, 500) {
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
