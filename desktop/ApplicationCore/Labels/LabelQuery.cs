using OrderManager.Domain.Labels;

namespace OrderManager.ApplicationCore.Labels;

public static class LabelQuery {

    public delegate Task<LabelFieldMap?> GetLabelById(int id);

    /// <summary>
    /// Returns a summary of all available label field maps
    /// </summary>
    public delegate Task<IEnumerable<LabelFieldMapSummary>> GetLabelSummaries();

    /// <summary>
    /// Returns a summary of all label field maps in a given profile
    /// </summary>
    public delegate Task<IEnumerable<LabelFieldMapSummary>> GetLabelSummariesByProfileId(int id);

    /// <summary>
    /// Returns a detailed label field map with a given id
    /// </summary>
    public delegate Task<LabelFieldMapDetails> GetLabelDetailsById(int id);

    /// <summary>
    /// Returns a detailed label field map for all labels in a given profile
    /// </summary>
    public delegate Task<IEnumerable<LabelFieldMapDetails>> GetLabelDetailsByProfileId(int id);

}
