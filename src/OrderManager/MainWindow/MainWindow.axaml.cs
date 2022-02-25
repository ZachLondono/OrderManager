using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using System.Diagnostics;
using System.Timers;

namespace OrderManager.MainWindow;
public partial class MainWindow : Window {

    bool _titlebarSecondClick = false;

    public MainWindow() {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.FindControl<Button>("PART_MinimizeButton").Click += delegate {
            WindowState = WindowState.Minimized;
        };
        this.FindControl<Button>("PART_MaximizeButton").Click += delegate {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        };
        this.FindControl<Button>("PART_CloseButton").Click += delegate {
            Close();
        };

        var titleBar = this.FindControl<Control>("PART_TitleBar");
        titleBar.PointerPressed += (object? sender, PointerPressedEventArgs ep) => {

            Debug.WriteLine("TitleBar pressed");
            if (_titlebarSecondClick)
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            else
                PlatformImpl?.BeginMoveDrag(ep);


            if (!_titlebarSecondClick) {
                _titlebarSecondClick = true;

                Timer secondClickTimer = new Timer(250);
                secondClickTimer.Elapsed += (sneder, e) => {
                    _titlebarSecondClick = false;
                    secondClickTimer.Stop();
                };
                secondClickTimer.Start();
            }
        };

        SetupSide("Left_top", StandardCursorType.LeftSide, WindowEdge.West);
        SetupSide("Left_mid", StandardCursorType.LeftSide, WindowEdge.West);
        SetupSide("Left_bottom", StandardCursorType.LeftSide, WindowEdge.West);
        SetupSide("Right_top", StandardCursorType.RightSide, WindowEdge.East);
        SetupSide("Right_mid", StandardCursorType.RightSide, WindowEdge.East);
        SetupSide("Right_bottom", StandardCursorType.RightSide, WindowEdge.East);
        SetupSide("Top", StandardCursorType.TopSide, WindowEdge.North);
        SetupSide("Bottom", StandardCursorType.BottomSide, WindowEdge.South);
        SetupSide("TopLeft", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest);
        SetupSide("TopRight", StandardCursorType.TopRightCorner, WindowEdge.NorthEast);
        SetupSide("BottomLeft", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest);
        SetupSide("BottomRight", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast);
    }

    private void SetupSide(string name, StandardCursorType cursor, WindowEdge edge) {
        var control = this.FindControl<Control>(name);
        control.Cursor = new(cursor);
        control.PointerPressed += (s, e) => {
            ((Window)this.GetVisualRoot()).PlatformImpl?.BeginResizeDrag(edge, e);
        };
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}
