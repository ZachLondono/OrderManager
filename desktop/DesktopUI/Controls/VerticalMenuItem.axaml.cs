using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace DesktopUI.Controls;
public class VerticalMenuItem : TemplatedControl {

    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<VerticalMenuItem, string>(nameof(Text));

    public string Text {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

}
