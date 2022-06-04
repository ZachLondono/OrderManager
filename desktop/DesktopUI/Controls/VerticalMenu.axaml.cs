using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;

namespace DesktopUI.Controls;

[PseudoClasses(pcCollapsed)]
public class VerticalMenu : TemplatedControl {

    public static readonly DirectProperty<VerticalMenu, ObservableCollection<VerticalMenuItem>> MenuItemsProperty =
                    AvaloniaProperty.RegisterDirect<VerticalMenu, ObservableCollection<VerticalMenuItem>>(nameof(MenuItems), o => o.MenuItems, (o, v) => o.MenuItems = v);

    public static readonly StyledProperty<int> MenuWidthProperty = AvaloniaProperty.Register<VerticalMenuItem, int>(nameof(MenuWidth));

    public static readonly StyledProperty<int> OpenWidthProperty = AvaloniaProperty.Register<VerticalMenuItem, int>(nameof(OpenWidth));

    public static readonly StyledProperty<int> CollapsedWidthProperty = AvaloniaProperty.Register<VerticalMenuItem, int>(nameof(CollapsedWidth));

    public static readonly StyledProperty<bool> IsOpenProperty = AvaloniaProperty.Register<VerticalMenuItem, bool>(nameof(IsOpen));


    private ObservableCollection<VerticalMenuItem> _items = new();
    public ObservableCollection<VerticalMenuItem> MenuItems {
        get => _items;
        set => SetAndRaise(MenuItemsProperty, ref _items, value);
    }

    public int MenuWidth {
        get => GetValue(MenuWidthProperty);
        set => SetValue(MenuWidthProperty, value);
    }

    /// <summary>
    /// The width of the Menu when in open mode
    /// </summary>
    public int OpenWidth {
        get => GetValue(OpenWidthProperty);
        set => SetValue(OpenWidthProperty, value);
    }

    /// <summary>
    /// The width of the Menu when in compact mode
    /// </summary>
    public int CollapsedWidth {
        get => GetValue(CollapsedWidthProperty);
        set => SetValue(CollapsedWidthProperty, value);
    }

    public bool IsOpen {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    protected const string pcCollapsed = ":collapsed";

    public VerticalMenu() {
        OpenWidth = 300;
        CollapsedWidth = 45;
        IsOpen = true;
    }

    private Button? _openButton = null;
    private Button? _closeButton = null;
    private ListBox? _listBox = null;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
        base.OnApplyTemplate(e);
        
        _openButton = e.NameScope.Find<Button>("OpenBtn");
        _closeButton = e.NameScope.Find<Button>("CloseBtn");

        if (_openButton is not null) _openButton.Click += ToggleMenuMode;
        if (_closeButton is not null) _closeButton.Click += ToggleMenuMode;

        _listBox = e.NameScope.Find<ListBox>("VerticalMenuListBox");
        if (_listBox is not null)
            _listBox.SelectionChanged += SelectionChanged;

    }

    private void SelectionChanged(object? sender, SelectionChangedEventArgs e) {
        var item = e.AddedItems[0] as VerticalMenuItem;
        if (item is not null) item.Command.Execute(null);
    }

    public void ToggleMenuMode(object? sender, RoutedEventArgs args) {
        IsOpen = !IsOpen;
        PseudoClasses.Set(pcCollapsed, !IsOpen);
    }

}
