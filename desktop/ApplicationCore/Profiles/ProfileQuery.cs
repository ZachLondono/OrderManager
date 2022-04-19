using OrderManager.Domain.Profiles;

namespace OrderManager.ApplicationCore.Profiles;

public static class ProfileQuery {

    public delegate Task<ReleaseProfile?> GetById(int id);

    public delegate Task<ReleaseProfileSummary> GetProfileSummaries();

    public delegate Task<ReleaseProfileDetails> GetProfileDetailsById(int id);

}
