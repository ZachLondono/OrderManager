using PluginContracts.Interfaces;
using PluginContracts.Models;

namespace TestReleaseAction;

public class ReleaseActionA : IReleaseAction {

    public string PluginName => "Action A";

    public int Version => 1;

    public void Run(OrderDto orderDto) {
        throw new NotImplementedException();
    }

}

public class ReleaseActionB : IReleaseAction {

    public string PluginName => "Action B";

    public int Version => 1;

    public void Run(OrderDto orderDto) {
        throw new NotImplementedException();
    }

}

public class ReleaseActionC : IReleaseAction {

    public string PluginName => "Action C";

    public int Version => 1;

    public void Run(OrderDto orderDto) {
        throw new NotImplementedException();
    }

}

public class ReleaseActionD : IReleaseAction {

    public string PluginName => "Action D";

    public int Version => 1;

    public void Run(OrderDto orderDto) {
        throw new NotImplementedException();
    }

}
