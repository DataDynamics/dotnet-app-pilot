using System.Collections.Generic;
using System.Reflection;
using DataDynamics.Common.Spring.Utils;

namespace DataDynamics.Common.Spring.Reflection;

#region IDynamicConstructor interface

/// <summary>
///     Defines constructors that dynamic constructor class has to implement.
/// </summary>
public interface IDynamicConstructor
{
    /// <summary>
    ///     Invokes dynamic constructor.
    /// </summary>
    /// <param name="arguments">
    ///     Constructor arguments.
    /// </param>
    /// <returns>
    ///     A constructor value.
    /// </returns>
    object Invoke(object[] arguments);
}

#endregion

#region Safe wrapper

/// <summary>
///     Safe wrapper for the dynamic constructor.
/// </summary>
/// <remarks>
///     <see cref="SafeConstructor" /> will attempt to use dynamic
///     constructor if possible, but it will fall back to standard
///     reflection if necessary.
/// </remarks>
public class SafeConstructor : IDynamicConstructor
{
    private readonly ConstructorDelegate constructor;
    private ConstructorInfo constructorInfo;

    /// <summary>
    ///     Creates a new instance of the safe constructor wrapper.
    /// </summary>
    /// <param name="constructorInfo">Constructor to wrap.</param>
    public SafeConstructor(ConstructorInfo constructorInfo)
    {
        this.constructorInfo = constructorInfo;
        constructor = GetOrCreateDynamicConstructor(constructorInfo);
    }


    /// <summary>
    ///     Invokes dynamic constructor.
    /// </summary>
    /// <param name="arguments">
    ///     Constructor arguments.
    /// </param>
    /// <returns>
    ///     A constructor value.
    /// </returns>
    public object Invoke(object[] arguments)
    {
        return constructor(arguments);
    }

    #region Generated Function Cache

    private static readonly IDictionary<ConstructorInfo, ConstructorDelegate> constructorCache =
        new Dictionary<ConstructorInfo, ConstructorDelegate>();

    /// <summary>
    ///     Obtains cached constructor info or creates a new entry, if none is found.
    /// </summary>
    private static ConstructorDelegate GetOrCreateDynamicConstructor(ConstructorInfo constructorInfo)
    {
        ConstructorDelegate method;
        if (!constructorCache.TryGetValue(constructorInfo, out method))
        {
            method = DynamicReflectionManager.CreateConstructor(constructorInfo);
            lock (constructorCache)
            {
                constructorCache[constructorInfo] = method;
            }
        }

        return method;
    }

    #endregion
}

#endregion

/// <summary>
///     Factory class for dynamic constructors.
/// </summary>
/// <author>Aleksandar Seovic</author>
public class DynamicConstructor : BaseDynamicMember
{
    /// <summary>
    ///     Creates dynamic constructor instance for the specified <see cref="ConstructorInfo" />.
    /// </summary>
    /// <param name="constructorInfo">Constructor info to create dynamic constructor for.</param>
    /// <returns>Dynamic constructor for the specified <see cref="ConstructorInfo" />.</returns>
    public static IDynamicConstructor Create(ConstructorInfo constructorInfo)
    {
        AssertUtils.ArgumentNotNull(constructorInfo, "You cannot create a dynamic constructor for a null value.");

        return new SafeConstructor(constructorInfo);
    }
}