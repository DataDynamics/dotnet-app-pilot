using System;
using System.Reflection;
using DataDynamics.Common.Spring.Resources;
using DataDynamics.Common.Spring.Utils;
using DataDynamics.Common.Utils;
using log4net;
using log4net.Config;

namespace DataDynamics.App;

internal static class Program
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

    /// <summary>
    /// 해당 애플리케이션의 주 진입점입니다.
    /// </summary>
    [STAThread]
    static void Main()
    {
        IResourceLoader loader = new ConfigurableResourceLoader();
        var resource = loader.GetResource("Config/log4net.config");
        Console.Out.WriteLine(IOUtils.ToString(resource));
        Console.Out.WriteLine(resource);
        XmlConfigurator.Configure(resource.File);

        logger.Info(Mediator.Instance.ToString());
    }
}