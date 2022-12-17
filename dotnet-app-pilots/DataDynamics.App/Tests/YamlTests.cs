using Config.Net;
using DataDynamics.App.Model;
using DataDynamics.Common.Spring.Resources;
using NUnit.Framework;

namespace DataDynamics.App.Tests;

public class YamlTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetValue()
    {
        IResourceLoader loader = new ConfigurableResourceLoader();
        var yamlConfig = loader.GetResource("Tests/config.yml");
        var builder = new ConfigurationBuilder<AppConfig>().UseEnvironmentVariables()
            .UseYamlFile(yamlConfig.File.FullName);
        var configuration = builder.Build();

        Assert.AreEqual("Test Application", configuration.Name);
    }
}