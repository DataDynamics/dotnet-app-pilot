using System.IO;

namespace DataDynamics.Common.Spring.Resources;

/// <summary>
///     Simple interface for objects that are sources for
///     <see cref="System.IO.Stream" />s.
/// </summary>
/// <remarks>
///     <p>
///         This is the base interface for the abstraction encapsulated by
///         Spring.NET's <see cref="IResource" /> interface.
///     </p>
/// </remarks>
/// <author>Juergen Hoeller</author>
/// <author>Rick Evans (.NET)</author>
/// <seealso cref="IResource" />
public interface IInputStreamSource
{
    /// <summary>
    ///     Return an <see cref="System.IO.Stream" /> for this resource.
    /// </summary>
    /// <remarks>
    ///     <note type="caution">
    ///         Clients of this interface must be aware that every access of this
    ///         property will create a <i>fresh</i> <see cref="System.IO.Stream" />;
    ///         it is the responsibility of the calling code to close any such
    ///         <see cref="System.IO.Stream" />.
    ///     </note>
    /// </remarks>
    /// <value>
    ///     An <see cref="System.IO.Stream" />.
    /// </value>
    /// <exception cref="System.IO.IOException">
    ///     If the stream could not be opened.
    /// </exception>
    Stream InputStream { get; }
}