using Avalonia.Controls;

namespace DesktopUI.Common;

public record DialogWindowContent(string Title, int Width, int Height, IControl Content);