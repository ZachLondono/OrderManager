using OrderManager.Domain.Profiles;

namespace OrderManager.ApplicationCore.Profiles;

public static class ProfileQuery {

    public delegate Task<ReleaseProfile?> GetProfileById(int id);

    /// <summary>
    /// Returns a summary of all available release profiles
    /// </summary>
    public delegate Task<IEnumerable<ReleaseProfileSummary>> GetProfileSummaries();

    /// <summary>
    /// Returns a detailed release profiles with a given id
    /// </summary>
    public delegate Task<ReleaseProfileDetails> GetProfileDetailsById(int id);

}
