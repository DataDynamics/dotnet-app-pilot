using DataDynamics.Common.Spring.Resources;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace DataDynamics.App.Tests;

public class Log4NetTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Init()
    {
        IResourceLoader loader = new ConfigurableResourceLoader();
        var resource = loader.GetResource("Tests/log4net.config");
        XmlConfigurator.Configure(resource.File);

        var logger = LogManager.GetLogger(typeof(Log4NetTests));
        logger.Info("Hello World");
    }
}