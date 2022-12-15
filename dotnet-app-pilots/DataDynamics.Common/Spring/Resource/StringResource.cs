using System.IO;
using System.Text;
using DataDynamics.Common.Utils;

namespace DataDynamics.Common.Spring.Resources;

/// <summary>
///     A <see cref="IResource" /> adapter implementation encapsulating a simple string.
/// </summary>
/// <author>Erich Eichinger</author>
public class StringResource : AbstractResource
{
    /// <summary>
    ///     Creates a new instance of the <see cref="StringResource" /> class.
    /// </summary>
    public StringResource(string content)
        : this(content, Encoding.Default, null)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="StringResource" /> class.
    /// </summary>
    public StringResource(string content, Encoding encoding)
        : this(content, encoding, null)
    {
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="StringResource" /> class.
    /// </summary>
    public StringResource(string content, Encoding encoding, string description)
    {
        AssertUtils.ArgumentNotNull(encoding, "encoding");

        Content = content == null ? string.Empty : content;
        Encoding = encoding;
        Description = description == null ? string.Empty : description;
    }

    /// <summary>
    ///     Get the <see cref="System.IO.Stream" /> to
    ///     for accessing this resource.
    /// </summary>
    public override Stream InputStream => new MemoryStream(Encoding.GetBytes(Content), false);

    /// <summary>
    ///     Returns a description for this resource.
    /// </summary>
    /// <value>
    ///     A description for this resource.
    /// </value>
    /// <seealso cref="IResource.Description" />
    public override string Description { get; }

    /// <summary>
    ///     This implementation always returns true
    /// </summary>
    public override bool IsOpen => true;

    /// <summary>
    ///     This implemementation always returns true
    /// </summary>
    public override bool Exists => true;

    /// <summary>
    ///     Gets the encoding used to create a byte stream of the <see cref="Content" /> string.
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    ///     Gets the content encapsulated by this <see cref="StringResource" />.
    /// </summary>
    public string Content { get; }

    #region Fields

    #endregion
}