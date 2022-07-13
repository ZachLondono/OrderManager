using DesktopUI.ViewModels;

namespace DesktopUI.Common;

public interface IAbstractFactory<T> {
    T Create();
}