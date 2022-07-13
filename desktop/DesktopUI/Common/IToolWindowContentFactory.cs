using Avalonia.Controls;

namespace DesktopUI.Common;

public interface IToolWindowContentFactory<T> where T : IControl {

    public ToolWindowContent Create();

}
