using OrderManager.Domain.Labels;

namespace OrderManager.ApplicationCore.Labels;

public interface ILabelFieldMapRepository {

    /// <summary>
    /// Creates a new LabelFieldMap and persists it
    /// </summary>
    /// <param name="name">Name of the new label map</param>
    /// <param name="templatePath">Path to the label template to which this map pertains</param>
    /// <param name="labelType">Type of label</param>
    /// <param name="fields">The fields used by this label and their values</param>
    /// <returns></returns>
    public Task<LabelFieldMapContext> Add(string name, string templatePath, LabelType labelType, Dictionary<string, string> fields);

    /// <summary>
    /// Removes a label map from storage
    /// </summary>
    /// <param name="id">The id of the label map to remove</param>
    /// <returns>A task for the operation</returns>
    public Task Remove(int id);

    /// <summary>
    /// Get the label map with the given id
    /// </summary>
    /// <param name="id">The id of the label map</param>
    /// <returns>A label map context to track changes to a label field map</returns>
    public Task<LabelFieldMapContext> GetById(int id);

    /// <summary>
    /// Persists all the changes made to the given label field map
    /// </summary>
    /// <param name="context">Represents the label map and tracks the applied changes</param>
    /// <returns>A task for the operation</returns>
    public Task Save(LabelFieldMapContext context);

}