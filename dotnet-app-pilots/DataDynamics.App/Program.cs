using DataDynamics.Common.Spring.Resources;
using log4net;
using log4net.Config;

namespace DataDynamics.App;

internal static class Program
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

    /// <summary>
    ///     해당 애플리케이션의 주 진입점입니다.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        IResourceLoader loader = new ConfigurableResourceLoader();
        var resource = loader.GetResource("Config/log4net.config");
        XmlConfigurator.Configure(resource.File);

        logger.Info("log4net이 초기화되었습니다.");
    }
}