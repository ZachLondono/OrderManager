namespace OrderManager.ApplicationCore.Profiles;

public interface IReleaseProfileRepository {

    public Task<ReleaseProfileContext> Add(string name);

    public Task Remove(int id);

    public Task<ReleaseProfileContext> GetById(int id);

    public Task Save(ReleaseProfileContext context);

}
