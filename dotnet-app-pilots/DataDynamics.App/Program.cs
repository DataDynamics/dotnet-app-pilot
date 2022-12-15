using System;
using System.Reflection;
using Config.Net;
using DataDynamics.App.Model;
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
    static void Main(string[] args)
    {
        IResourceLoader loader = new ConfigurableResourceLoader();
        var resource = loader.GetResource("Config/log4net.config");
        XmlConfigurator.Configure(resource.File);

        logger.Info("log4net이 초기화되었습니다.");

        var yamlConfig = loader.GetResource("Config/config.yml");

        logger.Info(string.Format("환경설정 파일 '{0}'을 로딩합니다.", yamlConfig.File.FullName));

        var builder = new ConfigurationBuilder<AppConfig>().UseEnvironmentVariables()
            .UseYamlFile(yamlConfig.File.FullName);
        var configuration = builder.Build();

        logger.Info(string.Format("애플리케이션명 : {0}", configuration.Name));
    }
}