using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;

namespace DesktopUI.Controls;
public partial class PageController : UserControl {

    public static readonly StyledProperty<int> PageCountProperty = AvaloniaProperty.Register<PageController, int>(nameof(PageCount));
    public static readonly StyledProperty<int> SelectedPageProperty = AvaloniaProperty.Register<PageController, int>(nameof(SelectedPage));

    public int PageCount {
        get => GetValue(PageCountProperty);
        set {
            SetValue(PageCountProperty, value);
            CreateRadioButtons();
        }
    }

    public int SelectedPage {
        get => GetValue(SelectedPageProperty);
        set {
            if (value <= 0 || value > PageCount) return;
            SetValue(SelectedPageProperty, value);
            SetPage(value);
        }
    }

    private readonly ItemsControl _pageListContainer;
    private IList<RadioButton> _buttons;

    public PageController() {
        InitializeComponent();
        _pageListContainer = this.Find<ItemsControl>("PageListContainer");
        _buttons = new List<RadioButton>();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    public void OnNextPageClick(object sender, RoutedEventArgs args) {
        SelectedPage++;
    }

    public void OnPrevPageClick(object sender, RoutedEventArgs args) {
        SelectedPage--;
    }

    public void OnPageChange(object? sender, RoutedEventArgs args) {
        if (sender is null) return;
        if (sender is RadioButton btn) {
            SelectedPage = _buttons.IndexOf(btn) + 1;
        }
    }

    private void SetPage(int page) {
        if (page > PageCount || page <= 0 || _buttons.Count < page) return;
        _buttons[page - 1].IsChecked = true;
        //OnPageChange(page);
    }

    private void CreateRadioButtons() {
        
        List<RadioButton> buttons = new();
        for (int i = 1; i <= PageCount; i++) {
            var btn = new RadioButton();
            btn.Content = i.ToString();
            btn.Classes = new("Page");
            btn.GroupName = "Pages";
            btn.FontSize = 18;
            btn.Click += OnPageChange;

            buttons.Add(btn);
        }

        _buttons = buttons;
        _pageListContainer.Items = buttons;
        SelectedPage = 1;

    } 

}
