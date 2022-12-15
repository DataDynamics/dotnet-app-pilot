using System.Configuration.Internal;

namespace DataDynamics.Common.Spring.Utils;

/// <summary>
///     Holds text position information for e.g. error reporting purposes.
/// </summary>
/// <seealso cref="ConfigXmlElement" />
/// <seealso cref="ConfigXmlAttribute" />
public interface ITextPosition : IConfigErrorInfo
{
    /// <summary>
    ///     Gets a string specifying the file/resource name related to the configuration details.
    /// </summary>
    new string Filename { get; }

    /// <summary>
    ///     Gets an integer specifying the line number related to the configuration details.
    /// </summary>
    new int LineNumber { get; }

    /// <summary>
    ///     Gets an integer specifying the line position related to the configuration details.
    /// </summary>
    int LinePosition { get; }
}