using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System.Windows.Input;

namespace DesktopUI.Controls;
public class VerticalMenuItem : TemplatedControl {

    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<VerticalMenuItem, string>(nameof(Text));

    public static readonly StyledProperty<ICommand> CommandProperty = AvaloniaProperty.Register<VerticalMenuItem, ICommand>(nameof(Command));

    public string Text {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

}
