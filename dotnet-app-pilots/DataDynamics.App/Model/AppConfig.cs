using Config.Net;

namespace DataDynamics.App.Model;

public interface AppConfig
{
    [Option(Alias = "app.name")] string Name { get; }
}