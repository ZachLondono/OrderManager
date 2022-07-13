using Avalonia.Controls;
using System;

namespace DesktopUI.Common;

public class ToolWindowContentFactory<T> : IToolWindowContentFactory<T> where T : IControl {

    private readonly Func<ToolWindowContent> _factory;

    public ToolWindowContentFactory(Func<ToolWindowContent> factory) {
        _factory = factory;
    }

    public ToolWindowContent Create() => _factory();
}
