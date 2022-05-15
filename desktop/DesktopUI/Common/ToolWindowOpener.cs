using Avalonia.Controls;
using DesktopUI.ViewModels;
using DesktopUI.Views;
using System;
using System.Threading.Tasks;

namespace DesktopUI.Common;

public record ToolWindowContent(string Title, int Width, int Height, IControl Content, Func<Task>? LoadData);

public class ToolWindowOpener {

    public static void Open(ToolWindowContent content) {
        var dialog = CreateWindow(content);
        dialog.Show();
    }

    public static async Task OpenDialog(ToolWindowContent content) {
        var dialog = CreateWindow(content);
        await dialog.ShowDialog(App.MainWindow);
    }

    /// <summary>
    /// Opens a tool window as a dialog of the direct parent window of the given control
    /// </summary>
    /// <param name="content">The content to display in the new tool window</param>
    /// <param name="control">The control from which to find the parent window</param>
    public static async Task OpenDialog(ToolWindowContent content, IControl control) {
        var dialog = CreateWindow(content);

        Window? window = GetParentWindow(control);

        if (window is null) dialog.Show();
        await dialog.ShowDialog(window);
    }
    
    private static Window? GetParentWindow(IControl control) {
        while (control is not null) {
            if (control is Window window) return window;
            if (control.Parent is null) return null;
            control = control.Parent;
        }
        return null;
    }

    private static ToolWindow CreateWindow(ToolWindowContent content) {
        var vm = new ToolWindowViewModel(content.Content, content.LoadData);
        _ = vm.LoadData();
        return new ToolWindow(content.Title, content.Width, content.Height) {
            DataContext = vm
        };

    }

}