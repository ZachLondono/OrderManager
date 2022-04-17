using OrderManager.ApplicationCore.Labels;

namespace Infrastructure.Labels;

public class DymoLabelPrinter : ILabelPrinter {
    public Task<IEnumerable<string>> GetLabelFields(string templatePath) {
        throw new NotImplementedException();
    }

    public Task PrintLabel(string templatePath, Dictionary<string, string> fields, int qty) {
        throw new NotImplementedException();
    }
}