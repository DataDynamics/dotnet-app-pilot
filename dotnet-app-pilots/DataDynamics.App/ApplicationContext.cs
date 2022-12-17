using DataDynamics.App.Model;
using log4net;

namespace DataDynamics.App;

public sealed class ApplicationContext
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(ApplicationContext));

    //private static 인스턴스 객체
    private static readonly Lazy<ApplicationContext> _instance = new(() => new ApplicationContext());

    private static AppConfig config;

    //private 생성자 
    private ApplicationContext()
    {
        logger.Info("ApplicationContext initialized...");
    }

    //public static 의 객체반환 함수
    public static ApplicationContext Instance => _instance.Value;

    public AppConfig Config { get; set; }

    public override string ToString()
    {
        return "ApplicationContext";
    }
}