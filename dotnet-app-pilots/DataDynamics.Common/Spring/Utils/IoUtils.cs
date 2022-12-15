using System;
using System.IO;
using DataDynamics.Common.Spring.Resources;

namespace DataDynamics.Common.Spring.Utils;

/// <summary>
///     Utility methods for IO handling
/// </summary>
public sealed class IOUtils
{
    private IOUtils()
    {
        throw new InvalidOperationException("instantiation not supported");
    }

    /// <summary>
    ///     Copies one stream into another.
    ///     (Don't forget to call <see cref="Stream.Flush" /> on the destination stream!)
    /// </summary>
    /// <remarks>
    ///     Does not close the input stream!
    /// </remarks>
    public static void CopyStream(Stream src, Stream dest)
    {
        var bufferSize = 2048;
        var buffer = new byte[bufferSize];

        var bytesRead = 0;
        while ((bytesRead = src.Read(buffer, 0, bufferSize)) > 0) dest.Write(buffer, 0, bytesRead);
    }

    /// <summary>
    ///     Reads a stream into a byte array.
    /// </summary>
    /// <remarks>
    ///     Does not close the input stream!
    /// </remarks>
    public static byte[] ToByteArray(Stream src)
    {
        var stm = new MemoryStream();
        CopyStream(src, stm);
        stm.Close();
        return stm.ToArray();
    }

    /// <summary>
    ///     Reads a stream into a string.
    /// </summary>
    /// <remarks>
    ///     Does not close the input stream!
    /// </remarks>
    public static string ToString(Stream src)
    {
        var stm = new MemoryStream();
        CopyStream(src, stm);
        stm.Close();
        byte[] buffer = stm.ToArray();
        return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///     Reads a resource into a string.
    /// </summary>
    /// <remarks>
    ///     Does not close the input stream!
    /// </remarks>
    public static string ToString(IResource resource)
    {
        Stream src = resource.InputStream;
        return ToString(src);
    }
}