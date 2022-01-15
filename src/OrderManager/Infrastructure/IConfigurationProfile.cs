namespace OrderManager.ApplicationCore.Infrastructure;

public interface IConfigurationProfile {

    public string OutputDirectory { get; protected set; }

    public void SetProfile(string profileName);

}