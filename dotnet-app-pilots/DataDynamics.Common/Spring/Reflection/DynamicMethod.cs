using System;
using System.Collections;
using System.Reflection;
using DataDynamics.Common.Spring.Utils;

namespace DataDynamics.Common.Spring.Reflection;

#region IDynamicMethod interface

/// <summary>
///     Defines methods that dynamic method class has to implement.
/// </summary>
public interface IDynamicMethod
{
    /// <summary>
    ///     Invokes dynamic method on the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to invoke method on.
    /// </param>
    /// <param name="arguments">
    ///     Method arguments.
    /// </param>
    /// <returns>
    ///     A method return value.
    /// </returns>
    object Invoke(object target, params object[] arguments);
}

#endregion

#region Safe wrapper

/// <summary>
///     Safe wrapper for the dynamic method.
/// </summary>
/// <remarks>
///     <see cref="SafeMethod" /> will attempt to use dynamic
///     method if possible, but it will fall back to standard
///     reflection if necessary.
/// </remarks>
public class SafeMethod : IDynamicMethod
{
    private readonly MethodInfo methodInfo;

    private readonly SafeMethodState state;

    /// <summary>
    ///     Creates a new instance of the safe method wrapper.
    /// </summary>
    /// <param name="methodInfo">Method to wrap.</param>
    public SafeMethod(MethodInfo methodInfo)
    {
        AssertUtils.ArgumentNotNull(methodInfo, "You cannot create a dynamic method for a null value.");

        state = (SafeMethodState)stateCache[methodInfo];
        if (state == null)
        {
            var newState = new SafeMethodState(DynamicReflectionManager.CreateMethod(methodInfo),
                new object[methodInfo.GetParameters().Length]
            );

            lock (stateCache.SyncRoot)
            {
                state = (SafeMethodState)stateCache[methodInfo];
                if (state == null)
                {
                    state = newState;
                    stateCache[methodInfo] = state;
                }
            }
        }

        this.methodInfo = methodInfo;
    }

    /// <summary>
    ///     Gets the class, that declares this method
    /// </summary>
    public Type DeclaringType => methodInfo.DeclaringType;

    /// <summary>
    ///     Invokes dynamic method.
    /// </summary>
    /// <param name="target">
    ///     Target object to invoke method on.
    /// </param>
    /// <param name="arguments">
    ///     Method arguments.
    /// </param>
    /// <returns>
    ///     A method return value.
    /// </returns>
    public object Invoke(object target, params object[] arguments)
    {
        // special case - when calling Invoke(null,null) it is undecidible if the second null is an argument or the argument array
        var nullArguments = state.nullArguments;
        if (arguments == null && nullArguments.Length == 1) arguments = nullArguments;
        var arglen = arguments == null ? 0 : arguments.Length;
        if (nullArguments.Length != arglen)
            throw new ArgumentException(string.Format(
                "Invalid number of arguments passed into method {0} - expected {1}, but was {2}", methodInfo.Name,
                nullArguments.Length, arglen));

        return state.method(target, arguments);
    }

    #region Generated Function Cache

    private class SafeMethodState
    {
        public readonly FunctionDelegate method;
        public readonly object[] nullArguments;

        public SafeMethodState(FunctionDelegate method, object[] nullArguments)
        {
            this.method = method;
            this.nullArguments = nullArguments;
        }
    }

    private class IdentityTable : Hashtable
    {
        protected override int GetHash(object key)
        {
            return key.GetHashCode();
        }

        protected override bool KeyEquals(object item, object key)
        {
            return ReferenceEquals(item, key);
        }
    }

    private static readonly Hashtable stateCache = new IdentityTable();

    #endregion
}

#endregion

/// <summary>
///     Factory class for dynamic methods.
/// </summary>
/// <author>Aleksandar Seovic</author>
public class DynamicMethod : BaseDynamicMember
{
    /// <summary>
    ///     Creates dynamic method instance for the specified <see cref="MethodInfo" />.
    /// </summary>
    /// <param name="method">Method info to create dynamic method for.</param>
    /// <returns>Dynamic method for the specified <see cref="MethodInfo" />.</returns>
    public static IDynamicMethod Create(MethodInfo method)
    {
        AssertUtils.ArgumentNotNull(method, "You cannot create a dynamic method for a null value.");

        IDynamicMethod dynamicMethod = new SafeMethod(method);
        return dynamicMethod;
    }
}