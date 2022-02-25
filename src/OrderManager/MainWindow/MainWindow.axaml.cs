using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
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
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}
