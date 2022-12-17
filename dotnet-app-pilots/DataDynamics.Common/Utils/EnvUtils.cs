using System;

namespace DataDynamics.Common.Utils;

public class EnvUtils
{
    public static string GetProperty(string key)
    {
        return (string)Environment.GetEnvironmentVariables()[key];
    }
}