using System.IO;
using System.Text;
using DataDynamics.Common.Utils;

namespace DataDynamics.Common.Spring.Resources;

/// <summary>
///     Holder that combines <see cref="IResource" /> with a specific encoding to be used for reading
///     from the resource
/// </summary>
/// <author>Juergen Hoeller</author>
/// <author>Erich Eichinger (.NET)</author>
public class EncodedResource
{
    /// <summary>
    ///     Create an encoded resource, autodetecting the encoding from the resource stream.
    /// </summary>
    /// <param name="resource"></param>
    public EncodedResource(IResource resource)
        : this(resource, null, true)
    {
        // noop
    }

    /// <summary>
    ///     Create an encoded resource, autodetecting the encoding from the resource stream.
    /// </summary>
    /// <param name="resource">the resource to read from. Must not be <c>null</c></param>
    /// <param name="autoDetectEncoding">
    ///     whether to autoDetect encoding from byte-order marks (
    ///     <see cref="M:StreamReader(Stream, Encoding, bool)" />)
    /// </param>
    public EncodedResource(IResource resource, bool autoDetectEncoding)
        : this(resource, null, autoDetectEncoding)
    {
        // noop
    }

    /// <summary>
    ///     Create an encoded resource using the specified encoding.
    /// </summary>
    /// <param name="resource">the resource to read from. Must not be <c>null</c></param>
    /// <param name="encoding">the encoding to use. If <c>null</c>, encoding will be autodetected.</param>
    /// <param name="autoDetectEncoding">
    ///     whether to autoDetect encoding from byte-order marks (
    ///     <see cref="M:StreamReader(Stream, Encoding, bool)" />)
    /// </param>
    public EncodedResource(IResource resource, Encoding encoding, bool autoDetectEncoding)
    {
        AssertUtils.ArgumentNotNull(resource, "resource");
        Resource = resource;
        Encoding = encoding;
        AutoDetectEncoding = autoDetectEncoding;
    }

    /// <summary>
    ///     Get the underlying resource
    /// </summary>
    public IResource Resource { get; }

    /// <summary>
    ///     Get the encoding to use for reading, if any. May be <c>null</c>
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    ///     whether to autoDetect encoding from byte-order marks (<see cref="M:StreamReader(Stream, Encoding, bool)" />)
    /// </summary>
    public bool AutoDetectEncoding { get; }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public TextReader OpenReader()
    {
        if (Encoding != null) return new StreamReader(Resource.InputStream, Encoding, AutoDetectEncoding);

        return new StreamReader(Resource.InputStream, AutoDetectEncoding);
    }

    /// <summary>
    ///     Determine whether <paramref name="obj" /> equals this instance.
    /// </summary>
    /// <returns>
    ///     <c>true</c> if obj is an <see cref="EncodedResource" /> and both
    ///     , <see cref="Resource" /> and <see cref="Encoding" /> are equal.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (!(obj is EncodedResource)) return false;

        var other = (EncodedResource)obj;
        return Equals(Resource, other.Resource)
               && Equals(Encoding, other.Encoding);
    }

    /// <summary>
    ///     Calculate the unique hash code for this instance.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Resource.GetHashCode();
    }

    /// <summary>
    ///     Get a textual description of the resource.
    /// </summary>
    public override string ToString()
    {
        return Resource.ToString();
    }
}