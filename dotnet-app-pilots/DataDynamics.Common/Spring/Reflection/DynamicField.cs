using System.Collections.Generic;
using System.Reflection;
using DataDynamics.Common.Spring.Utils;

namespace DataDynamics.Common.Spring.Reflection;

#region IDynamicField interface

/// <summary>
///     Defines methods that dynamic field class has to implement.
/// </summary>
public interface IDynamicField
{
    /// <summary>
    ///     Gets the value of the dynamic field for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to get field value from.
    /// </param>
    /// <returns>
    ///     A field value.
    /// </returns>
    object GetValue(object target);

    /// <summary>
    ///     Gets the value of the dynamic field for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to set field value on.
    /// </param>
    /// <param name="value">
    ///     A new field value.
    /// </param>
    void SetValue(object target, object value);
}

#endregion

#region Safe wrapper

/// <summary>
///     Safe wrapper for the dynamic field.
/// </summary>
/// <remarks>
///     <see cref="SafeField" /> will attempt to use dynamic
///     field if possible, but it will fall back to standard
///     reflection if necessary.
/// </remarks>
public class SafeField : IDynamicField
{
    private readonly FieldGetterDelegate getter;
    private readonly FieldSetterDelegate setter;

    /// <summary>
    ///     Creates a new instance of the safe field wrapper.
    /// </summary>
    /// <param name="field">Field to wrap.</param>
    public SafeField(FieldInfo field)
    {
        AssertUtils.ArgumentNotNull(field, "You cannot create a dynamic field for a null value.");

        FieldInfo = field;
        var fi = GetOrCreateDynamicField(field);
        getter = fi.Getter;
        setter = fi.Setter;
    }

    internal FieldInfo FieldInfo { get; }

    /// <summary>
    ///     Gets the value of the dynamic field for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to get field value from.
    /// </param>
    /// <returns>
    ///     A field value.
    /// </returns>
    public object GetValue(object target)
    {
        return getter(target);
    }

    /// <summary>
    ///     Gets the value of the dynamic field for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to set field value on.
    /// </param>
    /// <param name="value">
    ///     A new field value.
    /// </param>
    public void SetValue(object target, object value)
    {
        setter(target, value);
    }

    #region Cache

    private static readonly IDictionary<FieldInfo, DynamicFieldCacheEntry> fieldCache =
        new Dictionary<FieldInfo, DynamicFieldCacheEntry>();

    /// <summary>
    ///     Holds cached Getter/Setter delegates for a Field
    /// </summary>
    private class DynamicFieldCacheEntry
    {
        public readonly FieldGetterDelegate Getter;
        public readonly FieldSetterDelegate Setter;

        public DynamicFieldCacheEntry(FieldGetterDelegate getter, FieldSetterDelegate setter)
        {
            Getter = getter;
            Setter = setter;
        }
    }

    /// <summary>
    ///     Obtains cached fieldInfo or creates a new entry, if none is found.
    /// </summary>
    private static DynamicFieldCacheEntry GetOrCreateDynamicField(FieldInfo field)
    {
        DynamicFieldCacheEntry fieldInfo;
        if (!fieldCache.TryGetValue(field, out fieldInfo))
        {
            fieldInfo = new DynamicFieldCacheEntry(DynamicReflectionManager.CreateFieldGetter(field),
                DynamicReflectionManager.CreateFieldSetter(field));
            lock (fieldCache)
            {
                fieldCache[field] = fieldInfo;
            }
        }

        return fieldInfo;
    }

    #endregion
}

#endregion

/// <summary>
///     Factory class for dynamic fields.
/// </summary>
/// <author>Aleksandar Seovic</author>
public class DynamicField : BaseDynamicMember
{
    /// <summary>
    ///     Creates dynamic field instance for the specified <see cref="FieldInfo" />.
    /// </summary>
    /// <param name="field">Field info to create dynamic field for.</param>
    /// <returns>Dynamic field for the specified <see cref="FieldInfo" />.</returns>
    public static IDynamicField Create(FieldInfo field)
    {
        AssertUtils.ArgumentNotNull(field, "You cannot create a dynamic field for a null value.");

        IDynamicField dynamicField = new SafeField(field);
        return dynamicField;
    }
}