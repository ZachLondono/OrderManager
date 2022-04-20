using Infrastructure.Plugins;
using OrderManager.ApplicationCore.Common;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Infrastructure.UnitTests;

public class PluginTests {

    [Fact]
    public void Should_Get_Only_Valid_Plugins() {

        PluginManager manager = new PluginManager(new MockIO());
        string mockPath = "path/";

        var settings = manager.GetValidPluginSettings(mockPath);

        Assert.False(settings.ContainsKey("OtherDirectory\\InvalidSettings.dll"));
        Assert.False(settings.ContainsKey("OtherDirectory\\OtherDirectory.dll"));
        Assert.True(settings.ContainsKey("ValidPlugin\\ValidPlugin.dll"));
        Assert.NotNull(settings["ValidPlugin\\ValidPlugin.dll"].PluginSettings);

    }

    class MockIO : IFileIO {
        
        public IEnumerable<string> EnumerateDirectories(string path) {
            return new List<string> { "ValidPlugin", "InvalidSettings", "OtherDirectory"};
        }

        public bool Exists(string? path) {
            if (path is null) return false;
            string fileName = Path.GetFileName(path);
            switch (fileName) {
                case "ValidPlugin.dll":
                case "ValidPlugin-settings.json":
                case "InalidSettings.dll":
                case "InalidSettings-settings.dll":
                    return true;
                default:
                    return false;
            }
        }

        public string ReadAllText(string fileName) {
            fileName = Path.GetFileName(fileName);
            switch (fileName) {
                case "ValidPlugin-settings.json":
                    return @"
{
    ""PluginSettings"" : {
        ""TestReleaseAction.ReleaseActionA"": {
            ""Name"": ""Test Release Action A"",
            ""Version"": 1
        }
    }
}";
                case "InalidSettings-settings.dll":
                    return @"
{
    ""InvalidData""
}";

                default:
                    return string.Empty;
            }
        }

    }

}
