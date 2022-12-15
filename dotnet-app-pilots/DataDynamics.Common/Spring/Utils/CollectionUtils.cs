using System;
using System.Collections;
using System.Reflection;

namespace DataDynamics.Common.Spring.Utils;

/// <summary>
///     Miscellaneous collection utility methods.
/// </summary>
/// <remarks>
///     Mainly for internal use within the framework.
/// </remarks>
/// <author>Mark Pollack (.NET)</author>
public sealed class CollectionUtils
{
    /// <summary>
    ///     A callback method used for comparing to items.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="left">the first object to compare</param>
    /// <param name="right">the second object to compare</param>
    /// <returns>Value Condition Less than zero x is less than y. Zero x equals y. Greater than zero x is greater than y.</returns>
    /// <seealso cref="IComparer.Compare" />
    /// <seealso cref="CollectionUtils.StableSort(IEnumerable,CompareCallback)" />
    public delegate int CompareCallback(object left, object right);

    /// <summary>
    ///     Checks if the given array or collection has elements and none of the elements is null.
    /// </summary>
    /// <param name="collection">the collection to be checked.</param>
    /// <returns>true if the collection has a length and contains only non-null elements.</returns>
    public static bool HasElements(ICollection collection)
    {
        return ArrayUtils.HasElements(collection);
    }

    /// <summary>
    ///     Checks if the given array or collection is null or has no elements.
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool HasLength(ICollection collection)
    {
        return ArrayUtils.HasLength(collection);
    }

    /// <summary>
    ///     Determine whether a given collection only contains
    ///     a single unique object
    /// </summary>
    /// <param name="coll"></param>
    /// <returns></returns>
    public static bool HasUniqueObject(ICollection coll)
    {
        if (coll.Count == 0) return false;

        object candidate = null;
        foreach (var elem in coll)
            if (candidate == null)
                candidate = elem;
            else if (candidate != elem) return false;

        return true;
    }

    /// <summary>
    ///     Determines whether the <paramref name="collection" /> contains the specified <paramref name="element" />.
    /// </summary>
    /// <param name="collection">The collection to check.</param>
    /// <param name="element">The object to locate in the collection.</param>
    /// <returns><see lang="true" /> if the element is in the collection, <see lang="false" /> otherwise.</returns>
    public static bool Contains(IEnumerable collection, object element)
    {
        if (collection == null) return false;

        if (collection is IList) return ((IList)collection).Contains(element);

        if (collection is IDictionary) return ((IDictionary)collection).Contains(element);

        var method = collection.GetType().GetMethod("contains",
            BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        if (null != method) return (bool)method.Invoke(collection, new[] { element });

        foreach (var item in collection)
            if (Equals(item, element))
                return true;

        return false;
    }

    /// <summary>
    ///     Adds the specified <paramref name="element" /> to the specified <paramref name="collection" /> .
    /// </summary>
    /// <param name="collection">The collection to add the element to.</param>
    /// <param name="element">The object to add to the collection.</param>
    public static void Add(ICollection collection, object element)
    {
        Add((IEnumerable)collection, element);
    }

    /// <summary>
    ///     Adds the specified <paramref name="element" /> to the specified <paramref name="enumerable" /> .
    /// </summary>
    /// <param name="enumerable">The enumerable to add the element to.</param>
    /// <param name="element">The object to add to the collection.</param>
    public static void Add(IEnumerable enumerable, object element)
    {
        if (enumerable == null) throw new ArgumentNullException("enumerable", "Collection cannot be null.");

        if (enumerable is IList)
        {
            ((IList)enumerable).Add(element);
            return;
        }

        MethodInfo method;
        method = enumerable.GetType().GetMethod("add",
            BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        if (null == method)
            throw new InvalidOperationException("Enumerable type " + enumerable.GetType() +
                                                " does not implement a Add() method.");

        method.Invoke(enumerable, new[] { element });
    }

    /// <summary>
    ///     Determines whether the collection contains all the elements in the specified collection.
    /// </summary>
    /// <param name="targetCollection">The collection to check.</param>
    /// <param name="sourceCollection">Collection whose elements would be checked for containment.</param>
    /// <returns>true if the target collection contains all the elements of the specified collection.</returns>
    public static bool ContainsAll(ICollection targetCollection, ICollection sourceCollection)
    {
        if (targetCollection == null) throw new ArgumentNullException("targetCollection", "Collection cannot be null.");

        if (sourceCollection == null) throw new ArgumentNullException("sourceCollection", "Collection cannot be null.");

        if (sourceCollection.Count == 0 && targetCollection.Count > 1)
            return true;

        var sourceCollectionEnumerator = sourceCollection.GetEnumerator();

        var contains = false;

        MethodInfo method;
        method = targetCollection.GetType().GetMethod("containsAll",
            BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

        if (method != null)
        {
            contains = (bool)method.Invoke(targetCollection, new object[] { sourceCollection });
        }
        else
        {
            method = targetCollection.GetType().GetMethod("Contains",
                BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            if (method == null)
                throw new InvalidOperationException(
                    "Target collection does not implment a Contains() or ContainsAll() method.");

            while (sourceCollectionEnumerator.MoveNext())
                if ((contains = (bool)method.Invoke(targetCollection,
                        new[] { sourceCollectionEnumerator.Current })) == false)
                    break;
        }

        return contains;
    }

    /// <summary>
    ///     Removes all the elements from the target collection that are contained in the source collection.
    /// </summary>
    /// <param name="targetCollection">Collection where the elements will be removed.</param>
    /// <param name="sourceCollection">Elements to remove from the target collection.</param>
    public static void RemoveAll(ICollection targetCollection, ICollection sourceCollection)
    {
        if (targetCollection == null || sourceCollection == null)
            throw new ArgumentNullException("Collection cannot be null.");

        var al = ToArrayList(sourceCollection);

        MethodInfo method;
        method = targetCollection.GetType().GetMethod("removeAll",
            BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

        if (method != null)
        {
            method.Invoke(targetCollection, new object[] { al });
        }
        else
        {
            method = targetCollection.GetType().GetMethod("Remove",
                BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null,
                new Type[1] { typeof(object) }, null);
            var methodContains = targetCollection.GetType().GetMethod("Contains",
                BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            if (method == null)
                throw new InvalidOperationException(
                    "Target Collection must implement either a RemoveAll() or Remove() method.");

            if (methodContains == null)
                throw new InvalidOperationException("TargetCollection must implement a Contains() method.");

            var e = al.GetEnumerator();
            while (e.MoveNext())
            while ((bool)methodContains.Invoke(targetCollection, new[] { e.Current }))
                method.Invoke(targetCollection, new[] { e.Current });
        }
    }

    /// <summary>
    ///     Converts an <see cref="System.Collections.ICollection" />instance to an <see cref="System.Collections.ArrayList" />
    ///     instance.
    /// </summary>
    /// <param name="inputCollection">The <see cref="System.Collections.ICollection" /> instance to be converted.</param>
    /// <returns>
    ///     An <see cref="System.Collections.ArrayList" /> instance in which its elements are the elements of the
    ///     <see cref="System.Collections.ICollection" /> instance.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">if the <paramref name="inputCollection" /> is null.</exception>
    public static ArrayList ToArrayList(ICollection inputCollection)
    {
        if (inputCollection == null) throw new ArgumentNullException("Collection cannot be null.");

        return new ArrayList(inputCollection);
    }

    /// <summary>
    ///     Copies the elements of the <see cref="ICollection" /> to a
    ///     new array of the specified element type.
    /// </summary>
    /// <param name="inputCollection">The <see cref="System.Collections.ICollection" /> instance to be converted.</param>
    /// <param name="elementType">The element <see cref="Type" /> of the destination array to create and copy elements to</param>
    /// <returns>An array of the specified element type containing copies of the elements of the <see cref="ICollection" />.</returns>
    public static Array ToArray(ICollection inputCollection, Type elementType)
    {
        var array = Array.CreateInstance(elementType, inputCollection.Count);
        inputCollection.CopyTo(array, 0);
        return array;
    }

    /// <summary>
    ///     Returns the first element contained in both, <paramref name="source" /> and <paramref name="candidates" />.
    /// </summary>
    /// <remarks>
    ///     The implementation assumes that <paramref name="candidates" /> &lt;&lt;&lt; <paramref name="source" />
    /// </remarks>
    /// <param name="source">the source enumerable. may be <c>null</c></param>
    /// <param name="candidates">
    ///     the list of candidates to match against <paramref name="source" /> elements. may be
    ///     <c>null</c>
    /// </param>
    /// <returns>the first element found in both enumerables or <c>null</c></returns>
    public static object FindFirstMatch(IEnumerable source, IEnumerable candidates)
    {
        if (IsEmpty(source) || IsEmpty(candidates)) return null;

        var candidateList = candidates as IList;
        if (candidateList == null)
        {
            if (candidates is ICollection)
            {
                candidateList = new ArrayList((ICollection)candidates);
            }
            else
            {
                candidateList = new ArrayList();
                foreach (var el in candidates) candidateList.Add(el);
            }
        }

        foreach (var sourceElement in source)
            if (candidateList.Contains(sourceElement))
                return sourceElement;

        return null;
    }

    /// <summary>
    ///     Finds a value of the given type in the given collection.
    /// </summary>
    /// <param name="collection">The collection to search.</param>
    /// <param name="type">The type to look for.</param>
    /// <returns>a value of the given type found, or null if none.</returns>
    /// <exception cref="ArgumentException">If more than one value of the given type is found</exception>
    public static object FindValueOfType(ICollection collection, Type type)
    {
        if (IsEmpty(collection)) return null;

        var typeToUse = type != null ? type : typeof(object);
        object val = null;
        foreach (var obj in collection)
            if (typeToUse.IsAssignableFrom(obj.GetType()))
            {
                if (val != null)
                    throw new ArgumentException("More than one value of type[" + typeToUse.Name + "] found.");

                val = obj;
            }

        return val;
    }

    /// <summary>
    ///     Finds a value of the given type in the given collection.
    /// </summary>
    /// <param name="collection">The collection to search.</param>
    /// <param name="type">The type to look for.</param>
    /// <returns>
    ///     a collection of matching values of the given type found, empty if none found, or null if the input collection
    ///     was null.
    /// </returns>
    public static ICollection FindValuesOfType(IEnumerable collection, Type type)
    {
        if (IsEmpty(collection)) return null;

        var typeToUse = type != null ? type : typeof(object);
        var results = new ArrayList();
        foreach (var obj in collection)
            if (typeToUse.IsAssignableFrom(obj.GetType()))
                results.Add(obj);

        return results;
    }

    /// <summary>
    ///     Find a value of one of the given types in the given Collection,
    ///     searching the Collection for a value of the first type, then
    ///     searching for a value of the second type, etc.
    /// </summary>
    /// <param name="collection">The collection to search.</param>
    /// <param name="types">The types to look for, in prioritized order.</param>
    /// <returns>a value of the given types found, or <code>null</code> if none</returns>
    /// <exception cref="ArgumentException">If more than one value of the given type is found</exception>
    public static object FindValueOfType(ICollection collection, Type[] types)
    {
        if (IsEmpty(collection) || ObjectUtils.IsEmpty(types)) return null;

        foreach (var type in types)
        {
            var val = FindValueOfType(collection, type);
            if (val != null) return val;
        }

        return null;
    }

    /// <summary>
    ///     Determines whether the specified collection is null or empty.
    /// </summary>
    /// <param name="enumerable">The collection to check.</param>
    /// <returns>
    ///     <c>true</c> if the specified collection is empty or null; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsEmpty(IEnumerable enumerable)
    {
        if (enumerable == null)
            return true;

        if (enumerable is ICollection) return 0 == ((ICollection)enumerable).Count;

        var it = enumerable.GetEnumerator();
        if (!it.MoveNext()) return true;

        return false;
    }

    /// <summary>
    ///     Determines whether the specified collection is null or empty.
    /// </summary>
    /// <param name="collection">The collection to check.</param>
    /// <returns>
    ///     <c>true</c> if the specified collection is empty or null; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsEmpty(ICollection collection)
    {
        return collection == null || collection.Count == 0;
    }

    /// <summary>
    ///     Determines whether the specified dictionary is null empty.
    /// </summary>
    /// <param name="dictionary">The dictionary to check.</param>
    /// <returns>
    ///     <c>true</c> if the specified dictionary is empty or null; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsEmpty(IDictionary dictionary)
    {
        return dictionary == null || dictionary.Count == 0;
    }

    /// <summary>
    ///     A simple stable sorting routine - far from being efficient, only for small collections.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static ICollection StableSort(IEnumerable input, IComparer comparer)
    {
        return StableSort(input, comparer.Compare);
    }

    /// <summary>
    ///     A simple stable sorting routine - far from being efficient, only for small collections.
    /// </summary>
    /// <remarks>
    ///     Sorting is not(!) done in-place. Instead a sorted copy of the original input is returned.
    /// </remarks>
    /// <param name="input">input collection of items to sort</param>
    /// <param name="comparer">the <see cref="CompareCallback" /> for comparing 2 items in <paramref name="input" />.</param>
    /// <returns>a new collection of stable sorted items.</returns>
    public static ICollection StableSort(IEnumerable input, CompareCallback comparer)
    {
        var ehancedInput = new ArrayList();
        var it = input.GetEnumerator();
        var index = 0;
        while (it.MoveNext())
        {
            ehancedInput.Add(new Entry(index, it.Current));
            index++;
        }

        ehancedInput.Sort(Entry.GetComparer(comparer));

        for (var i = 0; i < ehancedInput.Count; i++) ehancedInput[i] = ((Entry)ehancedInput[i]).Value;

        return ehancedInput;
    }

    /// <summary>
    ///     A simple stable sorting routine - far from being efficient, only for small collections.
    /// </summary>
    /// <remarks>
    ///     Sorting is not(!) done in-place. Instead a sorted copy of the original input is returned.
    /// </remarks>
    /// <param name="input">input collection of items to sort</param>
    /// <param name="comparer">the <see cref="IComparer" /> for comparing 2 items in <paramref name="input" />.</param>
    /// <returns>a new collection of stable sorted items.</returns>
    public static void StableSortInPlace(IList input, IComparer comparer)
    {
        StableSortInPlace(input, comparer.Compare);
    }

    /// <summary>
    ///     A simple stable sorting routine - far from being efficient, only for small collections.
    /// </summary>
    /// <remarks>
    ///     Sorting is not(!) done in-place. Instead a sorted copy of the original input is returned.
    /// </remarks>
    /// <param name="input">input collection of items to sort</param>
    /// <param name="comparer">the <see cref="CompareCallback" /> for comparing 2 items in <paramref name="input" />.</param>
    /// <returns>a new collection of stable sorted items.</returns>
    public static void StableSortInPlace(IList input, CompareCallback comparer)
    {
        var ehancedInput = new ArrayList();
        var it = input.GetEnumerator();
        var index = 0;
        while (it.MoveNext())
        {
            ehancedInput.Add(new Entry(index, it.Current));
            index++;
        }

        ehancedInput.Sort(Entry.GetComparer(comparer));

        for (var i = 0; i < ehancedInput.Count; i++) input[i] = ((Entry)ehancedInput[i]).Value;
    }

    private class Entry
    {
        public readonly int Index;
        public readonly object Value;

        public Entry(int index, object value)
        {
            Index = index;
            Value = value;
        }

        public static IComparer GetComparer(CompareCallback innerComparer)
        {
            return new EntryComparer(innerComparer);
        }

        private class EntryComparer : IComparer
        {
            private readonly CompareCallback innerComparer;

            public EntryComparer(CompareCallback innerComparer)
            {
                this.innerComparer = innerComparer;
            }

            public int Compare(object x, object y)
            {
                var ex = (Entry)x;
                var ey = (Entry)y;
                var result = innerComparer(ex.Value, ey.Value);
                if (result == 0) result = ex.Index.CompareTo(ey.Index);

                return result;
            }
        }
    }
}