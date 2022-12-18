using System.Configuration;

namespace DataDynamics.App.Database;

internal static class Program
{
    /// <summary>
    ///     해당 애플리케이션의 주 진입점입니다.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {

        string appName = ConfigurationManager.AppSettings["AppName"];
        Console.Out.WriteLine(appName);
    }
}