using OrderManager.ApplicationCore.Common;
using OrderManager.Domain.Labels;
using OrderManager.Domain.Orders;

namespace OrderManager.ApplicationCore.Labels;

public class LabelService {

    private readonly ILabelFieldMapRepository _repo;
    private readonly ILabelPrinter _printer;

    public LabelService(ILabelFieldMapRepository repo, ILabelPrinter printer) {
        _repo = repo;
        _printer = printer;
    }

    /// <summary>
    /// Creates a default label field map from a template
    /// </summary>
    /// <param name="templatePath">The path to the template used to create the label field map</param>
    /// <param name="labelType">The type of label</param>
    /// <returns>An object used to track changes to the label field map</returns>
    public async Task<LabelFieldMapContext> CreateLabelFieldMap(string templatePath, LabelType labelType = LabelType.Order) {

        string? fileName = Path.GetFileNameWithoutExtension(templatePath);

        IEnumerable<string> fieldNames = await _printer.GetLabelFields(templatePath);

        Dictionary<string, string> fields = new();
        foreach (var fieldName in fieldNames) {
            fields.Add(fieldName, fieldName);
        }

        return await _repo.Add(fileName, templatePath, labelType, fields);

    }

    /// <summary>
    /// Prints all of the required labels for the given order and label map
    /// </summary>
    /// <param name="order">The order data used to fill in the label fields</param>
    /// <param name="map">The label field map used to fill in the label</param>
    public async Task PrintLabels(Order order, LabelFieldMap map) {
        
        if (map.Type is LabelType.Order) {

            Dictionary<string, string> filledFields = await EvaluateOrderLabelFormulas(order, map.Fields);
            await _printer.PrintLabel(map.TemplatePath, filledFields, map.PrintQty);

        } else if (map.Type is LabelType.LineItem) {

            foreach (var item in order.Items) {

                Dictionary<string, string> filledFields = await EvaluateProductLabelFormulas(order, item, map.Fields);
                await _printer.PrintLabel(map.TemplatePath, filledFields, item.Qty * map.PrintQty);

            }

        }

    }

    /// <summary>
    /// Prints a single label for a single line item in a given order
    /// </summary>
    /// <param name="order">The order to which in which the item exists</param>
    /// <param name="lineNumber">The number of the item from which the label data comes from</param>
    /// <param name="map">The label map used to fill in the label</param>
    public async Task PrintSingleItemLabel(Order order, int lineNumber, LabelFieldMap map) {
        
        LineItem? item = order.Items.Where(i => i.LineNumber == lineNumber).FirstOrDefault();
        if (item is null) throw new InvalidOperationException($"Line number '{lineNumber}' does not exist in order");

        Dictionary<string, string> filledFields = await EvaluateProductLabelFormulas(order, item, map.Fields);
        await _printer.PrintLabel(map.TemplatePath, filledFields, 1);

    }

    /// <summary>
    /// Prints a single label for an order
    /// </summary>
    /// <param name="order">The order data used to fill in the label</param>
    /// <param name="map">The field map used to fill in the label from the given data</param>
    public async Task PrintSingleOrderLabel(Order order, LabelFieldMap map) {
        Dictionary<string, string> filledFields = await EvaluateOrderLabelFormulas(order, map.Fields);
        await _printer.PrintLabel(map.TemplatePath, filledFields, 1);
    }

    public static async Task<Dictionary<string, string>> EvaluateOrderLabelFormulas(Order order, IEnumerable<KeyValuePair<string, string>> fields) {

        Dictionary<string, string> filledFields = new();

        foreach (var field in fields) {
            filledFields[field.Key] = await FormulaService.ExecuteFormula(field.Value, order, "order");
        }

        return filledFields;

    }

    public static async Task<Dictionary<string, string>> EvaluateProductLabelFormulas(Order order, LineItem item, IEnumerable<KeyValuePair<string, string>> fields) {

        Dictionary<string, string> filledFields = new();

        foreach (var field in fields) {
            filledFields[field.Key] = await FormulaService.ExecuteFormula(field.Value, order, item, "order", "item");
        }

        return filledFields;
    }

}