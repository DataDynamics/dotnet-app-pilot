using System;
using System.Reflection;
using System.Threading;

namespace DataDynamics.Common.Spring.Utils;

/// <summary>
///     Utility class containing miscellaneous system-level functionality.
/// </summary>
/// <author>Aleksandar Seovic</author>
public sealed class SystemUtils
{
    private static bool assemblyResolverRegistered;
    private static readonly object assemblyResolverLock;

    static SystemUtils()
    {
        MonoRuntime = Type.GetType("Mono.Runtime") != null;
        assemblyResolverLock = new object();
    }


    /// <summary>
    ///     Returns true if running on Mono
    /// </summary>
    /// <remarks>Tests for the presence of the type Mono.Runtime</remarks>
    public static bool MonoRuntime { get; }

    /// <summary>
    ///     Gets the thread id for the current thread. Use thread name is available,
    ///     otherwise use CurrentThread.GetHashCode() for .NET 1.0/1.1 and
    ///     CurrentThread.ManagedThreadId otherwise.
    /// </summary>
    /// <value>The thread id.</value>
    public static string ThreadId
    {
        get
        {
            var name = Thread.CurrentThread.Name;
            if (StringUtils.HasText(name))
                return name;
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }

    /// <summary>
    ///     Registers assembly resolver that iterates over the
    ///     assemblies loaded into the current <see cref="AppDomain" />
    ///     in order to find an assembly that cannot be resolved.
    /// </summary>
    /// <remarks>
    ///     This method has to be called if you need to serialize dynamically
    ///     generated types in transient assemblies, such as Spring AOP proxies,
    ///     because standard .NET serialization engine always tries to load
    ///     assembly from the disk.
    /// </remarks>
    public static void RegisterLoadedAssemblyResolver()
    {
        if (!assemblyResolverRegistered)
            lock (assemblyResolverLock)
            {
                AppDomain.CurrentDomain.AssemblyResolve += LoadedAssemblyResolver;
                assemblyResolverRegistered = true;
            }
    }

    private static Assembly LoadedAssemblyResolver(object sender, ResolveEventArgs args)
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in loadedAssemblies)
            if (assembly.FullName == args.Name)
                return assembly;

        return null;
    }
}