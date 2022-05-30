namespace Manufacturing.Contracts;

/// <summary>
/// Represents a Job which has yet to be scheduled and assigned to a Work Cell
/// </summary>
public record BackLogItem {

    /// <summary>
    /// The ID of the job which this item represents
    /// </summary>
    public int JobId { get; init; }

    /// <summary>
    /// The ID of the product class to which the items in this job belong
    /// </summary>
    public int ProductClass { get; init; }

    /// <summary>
    /// The quantity of items in the Job
    /// </summary>
    public int Qty { get; init; }

}
