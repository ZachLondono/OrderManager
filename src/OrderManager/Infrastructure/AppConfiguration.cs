using Microsoft.Extensions.Configuration;

namespace OrderManager.ApplicationCore.Infrastructure;

public class AppConfiguration {

    private readonly IConfiguration _config;

    public AppConfiguration(IConfiguration config) {
        _config = config;
    }

    public string? CatalogConnectionString {
        get => _config.GetConnectionString("AccessDB"); 
    }

    public string? OrderConnectionString { 
        get => _config.GetConnectionString("AccessDB");
    }

    public string? ScriptDirectory { 
        get => _config["ScriptDir"];
        set => _config["ScriptDir"] = value;
    }

    public string? ExcelPrinterExecutable { 
        get => _config["ConverterPath"];
        set => _config["ConverterPath"] = value;
    }

}
