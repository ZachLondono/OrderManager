namespace Manufacturing.Contracts;

/// <summary>
/// A collection of delegates which do operations regarding the WorkShop
/// </summary>
public class WorkShop {
    
    /// <summary>
    /// Retrieve a Back Log of Jobs which have not yet been assigned to a Work Cell
    /// </summary>
    /// <returns>A list of BackLogItems which represent Jobs in the Back Log</returns>
    public delegate Task<BackLog> GetBackLog();

    /// <summary>
    /// Retrieves the ID of the WorkCell which the job is scheduled to be made in
    /// </summary>
    /// <param name="JobId">The ID of the Job to check for</param>
    /// <returns>The ID of the WorkCell which contains the Job</returns>
    public delegate Task<int?> GetJobWorkCellId(int JobId);
}
