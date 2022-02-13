using OrderManager.Shared;

namespace OrderManager.Shared.DataError;

public class DataErrorViewModel : ViewModelBase {

    public string Message { get; init; }

    public string DetailedMessage { get; init; }

    public DataErrorViewModel(string message, string detailedMessage) {
        Message = message;
        DetailedMessage = detailedMessage;
    }

}
