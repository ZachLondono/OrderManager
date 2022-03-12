using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OrderManager.Features.DebugWindow;
public partial class ConsoleWindow : Window {
    public ConsoleWindow() {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        var textbox = this.FindControl<TextBox>("ConsoleOutput");
        var outputter = new DebugOutputter(textbox);

        System.Console.SetOut(outputter);

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
