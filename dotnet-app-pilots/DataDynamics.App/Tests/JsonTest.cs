using Newtonsoft.Json;
using NUnit.Framework;

namespace DataDynamics.App.Tests;

public class JsonTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetValue()
    {
        var json = File.ReadAllText("Tests/config.json");
        Assert.IsNotEmpty(json);

        var config = JsonConvert.DeserializeObject<dynamic>(json);
        Assert.AreEqual("TestApp", config.name.ToString());
    }
}