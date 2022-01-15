namespace OrderManager.ApplicationCore.Infrastructure.Implementations;

internal class DBConfigurationProfile : IConfigurationProfile {
    
    public string? OutputDirectory { get; set; }

    public void SetProfile(string profileName) {
        throw new NotImplementedException();
    }


    private readonly DataBaseConfiguration _dbConfig;
    public DBConfigurationProfile(DataBaseConfiguration dbConfig) {
        _dbConfig = dbConfig;
    }

}
