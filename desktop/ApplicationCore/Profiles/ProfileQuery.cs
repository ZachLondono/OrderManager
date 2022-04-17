using OrderManager.Domain.Profiles;

namespace OrderManager.ApplicationCore.Profiles;

public static class ProfileQuery {

    public delegate Task<ReleaseProfile?> GetById(int id);

}
