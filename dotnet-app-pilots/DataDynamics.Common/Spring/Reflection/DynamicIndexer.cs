namespace DataDynamics.Common.Spring.Reflection;

/// <summary>
///     Defines methods that dynamic indexer class has to implement.
/// </summary>
public interface IDynamicIndexer
{
    /// <summary>
    ///     Gets the value of the dynamic indexer for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to get the indexer value from.
    /// </param>
    /// <param name="index">
    ///     Indexer argument.
    /// </param>
    /// <returns>
    ///     A indexer value.
    /// </returns>
    object GetValue(object target, int index);

    /// <summary>
    ///     Gets the value of the dynamic indexer for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to get the indexer value from.
    /// </param>
    /// <param name="index">
    ///     Indexer argument.
    /// </param>
    /// <returns>
    ///     A indexer value.
    /// </returns>
    object GetValue(object target, object index);

    /// <summary>
    ///     Gets the value of the dynamic indexer for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to get the indexer value from.
    /// </param>
    /// <param name="index">
    ///     Indexer arguments.
    /// </param>
    /// <returns>
    ///     A indexer value.
    /// </returns>
    object GetValue(object target, object[] index);

    /// <summary>
    ///     Gets the value of the dynamic indexer for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to set the indexer value on.
    /// </param>
    /// <param name="index">
    ///     Indexer argument.
    /// </param>
    /// <param name="value">
    ///     A new indexer value.
    /// </param>
    void SetValue(object target, int index, object value);

    /// <summary>
    ///     Gets the value of the dynamic indexer for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to set the indexer value on.
    /// </param>
    /// <param name="index">
    ///     Indexer argument.
    /// </param>
    /// <param name="value">
    ///     A new indexer value.
    /// </param>
    void SetValue(object target, object index, object value);

    /// <summary>
    ///     Gets the value of the dynamic indexer for the specified target object.
    /// </summary>
    /// <param name="target">
    ///     Target object to set the indexer value on.
    /// </param>
    /// <param name="index">
    ///     Indexer arguments.
    /// </param>
    /// <param name="value">
    ///     A new indexer value.
    /// </param>
    void SetValue(object target, object[] index, object value);
}