namespace OrderManager.ApplicationCore.Labels;

public interface ILabelPrinter {

    /// <summary>
    /// Prints a label of the given quantity with the fields filled in as provided
    /// </summary>
    /// <param name="templatePath">Path to label file to fill and print</param>
    /// <param name="fields">Map of template field to value</param>
    /// <param name="qty">Number of labels to print</param>
    /// <returns></returns>
    public Task PrintLabel(string templatePath, Dictionary<string, string> fields, int qty);

    /// <summary>
    /// Loads the label field names from a given label template
    /// </summary>
    /// <param name="templatePath">The path to the label template from which to load the fields</param>
    /// <returns></returns>
    public Task<IEnumerable<string>> GetLabelFields(string templatePath);

}