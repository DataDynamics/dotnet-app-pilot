using System;
using System.IO;
using System.Text;
using DataDynamics.Common.Spring.Utils;

namespace DataDynamics.Common.Spring.Resources;

/// <summary>
///     Convenience base class for <see cref="IResource" />
///     implementations, pre-implementing typical behavior.
/// </summary>
/// <remarks>
///     <p>
///         The <see cref="AbstractResource.Exists" /> method will
///         check whether a <see cref="System.IO.FileInfo" /> or
///         <see cref="System.IO.Stream" /> can be opened;
///         <see cref="AbstractResource.IsOpen" /> will always return
///         <see langword="false" />;
///         <see cref="System.Uri" /> and
///         <see cref="AbstractResource.File" /> throw an exception;
///         and <see cref="AbstractResource.ToString()" /> will
///         return the value of the
///         <see cref="IResource.Description" /> property.
///     </p>
/// </remarks>
/// <author>Juergen Hoeller</author>
/// <author>Rick Evans (.NET)</author>
/// <author>Aleksandar Seovic (.NET)</author>
/// <seealso cref="IResource" />
public abstract class AbstractResource : IResource
{
    /// <summary>
    ///     The default special character that denotes the base (home, or root)
    ///     path.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         Will be resolved (by those <see cref="IResource" />
    ///         implementations that support it) to the home (or root) path for
    ///         the specific <see cref="IResource" /> implementation.
    ///     </p>
    ///     <p>
    ///         For example, in the case of a web application this will (probably)
    ///         resolve to the virtual directory of said web application.
    ///     </p>
    /// </remarks>
    protected const string DefaultBasePathPlaceHolder = "~";

    private readonly string resourceName;

    #region Constructor (s) / Destructor

    /// <summary>
    ///     Creates a new instance of the
    ///     <see cref="AbstractResource" /> class.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This is an <see langword="abstract" /> class, and as such exposes no
    ///         public constructors.
    ///     </p>
    /// </remarks>
    protected AbstractResource()
    {
    }

    /// <summary>
    ///     Creates a new instance of the
    ///     <see cref="AbstractResource" /> class.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This is an <see langword="abstract" /> class, and as such exposes no
    ///         public constructors.
    ///     </p>
    /// </remarks>
    /// <param name="resourceName">
    ///     A string representation of the resource.
    /// </param>
    /// <exception cref="System.ArgumentNullException">
    ///     If the supplied <paramref name="resourceName" /> is
    ///     <see langword="null" /> or contains only whitespace character(s).
    /// </exception>
    protected AbstractResource(string resourceName)
    {
        AssertUtils.ArgumentHasText(resourceName, "resourceName");
        Protocol = ConfigurableResourceLoader.GetProtocol(resourceName);
        this.resourceName = resourceName;
    }

    #endregion

    #region Properties

    /// <summary>
    ///     The special character that denotes the base (home, or root)
    ///     path.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         Will be resolved (by those <see cref="IResource" />
    ///         implementations that support it) to the home (or root) path for
    ///         the specific <see cref="IResource" /> implementation.
    ///     </p>
    ///     <p>
    ///         For example, in the case of a web application this will (probably)
    ///         resolve to the virtual directory of said web application.
    ///     </p>
    /// </remarks>
    /// <seealso cref="AbstractResource.DefaultBasePathPlaceHolder" />
    public string BasePathPlaceHolder { get; set; } = DefaultBasePathPlaceHolder;

    /// <summary>
    ///     Return an <see cref="System.IO.Stream" /> for this resource.
    /// </summary>
    /// <value>
    ///     An <see cref="System.IO.Stream" />.
    /// </value>
    /// <exception cref="System.IO.IOException">
    ///     If the stream could not be opened.
    /// </exception>
    /// <seealso cref="IInputStreamSource.InputStream" />
    public abstract Stream InputStream { get; }

    /// <summary>
    ///     Returns a description for this resource.
    /// </summary>
    /// <value>
    ///     A description for this resource.
    /// </value>
    /// <seealso cref="IResource.Description" />
    public abstract string Description { get; }

    /// <summary>
    ///     Returns the protocol associated with this resource (if any).
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         The value of this property may be <see langword="null" /> if no
    ///         protocol is associated with the resource type (for example if the
    ///         resource is a memory stream).
    ///     </p>
    /// </remarks>
    /// <value>
    ///     The protocol associated with this resource (if any).
    /// </value>
    public string Protocol { get; }

    /// <summary>
    ///     Does this resource represent a handle with an open stream?
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This, the default implementation, always returns
    ///         <see langword="false" />.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     <see langword="true" /> if this resource represents a handle with an
    ///     open stream.
    /// </value>
    /// <seealso cref="IResource.IsOpen" />
    public virtual bool IsOpen => false;

    /// <summary>
    ///     Returns the <see cref="System.Uri" /> handle for this resource.
    /// </summary>
    /// <seealso cref="System.Uri" />
    public virtual Uri Uri => new(resourceName);

    /// <summary>
    ///     Returns a <see cref="System.IO.FileInfo" /> handle for this resource.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This, the default implementation, always throws a
    ///         <see cref="System.IO.FileNotFoundException" />, assuming that the
    ///         resource cannot be resolved to an absolute file path.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     The <see cref="System.IO.FileInfo" /> handle for this resource.
    /// </value>
    /// <exception cref="System.IO.FileNotFoundException">
    ///     This implementation <b>always</b> throws a
    ///     <see cref="System.IO.FileNotFoundException" />.
    /// </exception>
    /// <seealso cref="IResource" />
    /// <see cref="IResource.Exists" />
    public virtual FileInfo File =>
        throw new FileNotFoundException(
            Description + " cannot be resolved to an absolute file path.");

    /// <summary>
    ///     Does this resource actually exist in physical form?
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This implementation checks whether a <see cref="System.IO.FileInfo" />
    ///         can be opened, falling back to whether a
    ///         <see cref="System.IO.Stream" /> can be opened.
    ///     </p>
    ///     <p>
    ///         This will cover both directories and content resources.
    ///     </p>
    ///     <p>
    ///         This implementation will also return <see langword="false" /> if
    ///         permission to the (file's) path is denied.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     <see langword="true" /> if this resource actually exists in physical
    ///     form (for example on a filesystem).
    /// </value>
    /// <seealso cref="IResource.Exists" />
    /// <seealso cref="IResource.File" />
    public virtual bool Exists
    {
        get
        {
            try
            {
                return File.Exists;
            }
            catch (IOException)
            {
                try
                {
                    var inputStream = InputStream;
                    inputStream.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Strips any protocol name from the supplied
    ///     <paramref name="resourceName" />.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         If the supplied <paramref name="resourceName" /> does not
    ///         have any protocol associated with it, then the supplied
    ///         <paramref name="resourceName" /> will be returned as-is.
    ///     </p>
    /// </remarks>
    /// <example>
    ///     <code language="C#">
    ///  GetResourceNameWithoutProtocol("http://www.mycompany.com/resource.txt");
    /// 		// returns www.mycompany.com/resource.txt
    ///  </code>
    /// </example>
    /// <param name="resourceName">
    ///     The name of the resource.
    /// </param>
    /// <returns>
    ///     The name of the resource without the protocol name.
    /// </returns>
    protected static string GetResourceNameWithoutProtocol(string resourceName)
    {
        var pos = resourceName.IndexOf(
            ConfigurableResourceLoader.ProtocolSeparator);
        if (pos == -1)
            return resourceName;
        return resourceName.Substring(pos + ConfigurableResourceLoader.ProtocolSeparator.Length);
    }

    /// <summary>
    ///     Resolves the supplied <paramref name="resourceName" /> to its value
    ///     sans any leading protocol.
    /// </summary>
    /// <param name="resourceName">
    ///     The name of the resource.
    /// </param>
    /// <returns>
    ///     The name of the resource without the protocol name.
    /// </returns>
    /// <see cref="AbstractResource.GetResourceNameWithoutProtocol" />
    protected virtual string ResolveResourceNameWithoutProtocol(string resourceName)
    {
        return ResolveBasePathPlaceHolder(
            GetResourceNameWithoutProtocol(resourceName), BasePathPlaceHolder);
    }

    /// <summary>
    ///     Resolves the presence of the
    ///     <paramref name="basePathPlaceHolder" /> value
    ///     in the supplied <paramref name="resourceName" /> into a path.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         The default implementation simply returns the supplied
    ///         <paramref name="resourceName" /> as is.
    ///     </p>
    /// </remarks>
    /// <param name="resourceName">
    ///     The name of the resource.
    /// </param>
    /// <param name="basePathPlaceHolder">
    ///     The string that is a placeholder for a base path.
    /// </param>
    /// <returns>
    ///     The name of the resource with any <paramref name="basePathPlaceHolder" />
    ///     value having been resolved into an actual path.
    /// </returns>
    protected virtual string ResolveBasePathPlaceHolder(
        string resourceName, string basePathPlaceHolder)
    {
        return resourceName;
    }

    /// <summary>
    ///     This implementation returns the
    ///     <see cref="Description" /> of this resource.
    /// </summary>
    /// <seealso cref="IResource.Description" />
    public override string ToString()
    {
        return Description;
    }

    /// <summary>
    ///     Determines whether the specified <see cref="System.Object" /> is
    ///     equal to the current <see cref="System.Object" />.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This implementation compares <see cref="Description" /> values.
    ///     </p>
    /// </remarks>
    /// <seealso cref="IResource.Description" />
    public override bool Equals(object obj)
    {
        return obj is IResource
               && ((IResource)obj).Description.Equals(Description);
    }

    /// <summary>
    ///     Serves as a hash function for a particular type, suitable for use
    ///     in hashing algorithms and data structures like a hash table.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This implementation returns the hashcode of the
    ///         <see cref="Description" /> property.
    ///     </p>
    /// </remarks>
    /// <seealso cref="IResource.Description" />
    public override int GetHashCode()
    {
        return Description.GetHashCode();
    }

    #endregion

    #region Relative Resource Support

    /// <summary>
    ///     Factory Method. Create a new instance of the current resource type using the given resourceName
    /// </summary>
    protected virtual IResource CreateResourceInstance(string resourceName)
    {
        return null;
    }

    /// <summary>
    ///     The ResourceLoader to be used for resolving relative resources
    /// </summary>
    protected virtual IResourceLoader GetResourceLoader()
    {
        return new ConfigurableResourceLoader();
    }

    /// <summary>
    ///     Does this <see cref="IResource" /> support relative
    ///     resource retrieval?
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This property is generally to be consulted prior to attempting
    ///         to attempting to access a resource that is relative to this
    ///         resource (via a call to
    ///         <see cref="IResource.CreateRelative" />).
    ///     </p>
    ///     <p>
    ///         This, the default implementation, always returns
    ///         <see langword="false" />.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     <see langword="true" /> if this
    ///     <see cref="IResource" /> supports relative resource
    ///     retrieval.
    /// </value>
    protected virtual bool SupportsRelativeResources => false;

    /// <summary>
    ///     Gets the root location of the resource.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         Where root resource can be taken to mean that part of the resource
    ///         descriptor that doesn't change when a relative resource is looked
    ///         up. Examples of such a root location would include a drive letter,
    ///         a web server name, an assembly name, etc.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     The root location of the resource.
    /// </value>
    /// <exception cref="System.NotSupportedException">
    ///     This, the default implementation, <b>always</b> throws a
    ///     <see cref="System.NotSupportedException" />.
    /// </exception>
    protected virtual string RootLocation => throw new NotSupportedException();

    /// <summary>
    ///     Gets the current path of the resource.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         An example value of this property would be the name of the
    ///         directory containing a filesystem based resource.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     The current path of the resource.
    /// </value>
    /// <exception cref="System.NotSupportedException">
    ///     This, the default implementation, <b>always</b> throws a
    ///     <see cref="System.NotSupportedException" />.
    /// </exception>
    protected virtual string ResourcePath => throw new NotSupportedException();

    /// <summary>
    ///     Gets those characters that are valid path separators for the
    ///     resource type.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         An example value of this property would be the
    ///         <see cref="System.IO.Path.DirectorySeparatorChar" /> and
    ///         <see cref="System.IO.Path.AltDirectorySeparatorChar" /> values for a
    ///         filesystem based resource.
    ///     </p>
    ///     <p>
    ///         Any derived classes that override this method are expected to
    ///         return a new array for each access of this property.
    ///     </p>
    /// </remarks>
    /// <value>
    ///     Those characters that are valid path separators for the resource
    ///     type.
    /// </value>
    /// <exception cref="System.NotSupportedException">
    ///     This, the default implementation, <b>always</b> throws a
    ///     <see cref="System.NotSupportedException" />.
    /// </exception>
    protected virtual char[] PathSeparatorChars => throw new NotSupportedException();

    /// <summary>
    ///     Does the supplied <paramref name="resourceName" /> relative ?
    /// </summary>
    /// <param name="resourceName">
    ///     The name of the resource to test.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if resource name is relative;
    ///     otherwise <see langword="false" />.
    /// </returns>
    protected virtual bool IsRelativeResource(string resourceName)
    {
        return false;
    }

    /// <summary>
    ///     Creates a new resource that is relative to this resource based on the
    ///     supplied <paramref name="resourceName" />.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This method can accept either a fully qualified resource name or a
    ///         relative resource name as it's parameter.
    ///     </p>
    ///     <p>
    ///         A fully qualified resource is one that has a protocol prefix and
    ///         all elements of the resource name. All other resources are treated
    ///         as relative to this resource, and the following rules are used to
    ///         locate a relative resource:
    ///     </p>
    ///     <list type="bullet">
    ///         <item>
    ///             If the <paramref name="resourceName" /> starts with <c>'..'</c>,
    ///             the current resource path is navigated backwards before the
    ///             <paramref name="resourceName" /> is concatenated to the current
    ///             <see cref="AbstractResource.ResourcePath" /> of
    ///             this resource.
    ///         </item>
    ///         <item>
    ///             If the <paramref name="resourceName" /> starts with '/', the
    ///             current resource path is ignored and a new resource name is
    ///             appended to the
    ///             <see cref="AbstractResource.RootLocation" /> of
    ///             this resource.
    ///         </item>
    ///         <item>
    ///             If the <paramref name="resourceName" /> starts with '.' or a
    ///             letter, a new path is appended to the current
    ///             <see cref="AbstractResource.ResourcePath" /> of
    ///             this resource.
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <param name="resourceName">
    ///     The name of the resource to create.
    /// </param>
    /// <returns>The relative resource.</returns>
    /// <exception cref="System.UriFormatException">
    ///     If the process of resolving the relative resource yielded an
    ///     invalid URI.
    /// </exception>
    /// <exception cref="System.NotSupportedException">
    ///     If this resource does not support the resolution of relative
    ///     resources (as determined by the value of the
    ///     <see cref="AbstractResource.SupportsRelativeResources" />
    ///     property).
    /// </exception>
    /// <seealso cref="AbstractResource.ResourcePath" />
    public virtual IResource CreateRelative(string resourceName)
    {
        AssertUtils.ArgumentNotNull(resourceName, "relativePath");

        // try to create fully qualified resource...
        var loader = GetResourceLoader();

        if (ConfigurableResourceLoader.HasProtocol(resourceName))
        {
            var resource = loader.GetResource(resourceName);
            if (resource != null) return resource;
        }

        if (!SupportsRelativeResources)
            throw new NotSupportedException(GetType().Name +
                                            " does not support relative resources. Please use fully qualified resource name.");

        var fullResourceName = new StringBuilder(256);
        if (Protocol != null && Protocol != string.Empty)
            fullResourceName.Append(Protocol).Append(ConfigurableResourceLoader.ProtocolSeparator);

        if (!IsRelativeResource(resourceName))
        {
            fullResourceName.Append(resourceName);
        }
        else
        {
            string targetResource;
            string resourcePath;
            var n = resourceName.LastIndexOfAny(new[] { '/', '\\' });
            if (n >= 0)
            {
                targetResource = resourceName.Substring(n + 1);
                resourcePath = CalculateResourcePath(resourceName.Substring(0, n + 1));
            }
            else // only resource name is specified, so current path should be used
            {
                targetResource = resourceName;
                resourcePath = ResourcePath;
            }

            fullResourceName.Append(RootLocation.TrimEnd('\\', '/'));
            if (resourcePath != null && resourcePath != string.Empty) fullResourceName.Append('/').Append(resourcePath);
            fullResourceName.Append('/').Append(targetResource);
        }

        var resultResourceName = fullResourceName.ToString();

        if (!ConfigurableResourceLoader.HasProtocol(resultResourceName))
        {
            // give derived resource classes a chance to create an instance on their own
            var resultResource = CreateResourceInstance(resultResourceName);
            if (resultResource != null) return resultResource;
        }

        // create resource instance using default loader
        return loader.GetResource(resultResourceName);
    }

    /// <summary>
    ///     Calculates a new resource path based on the supplied
    ///     <paramref name="relativePath" />.
    /// </summary>
    /// <param name="relativePath">
    ///     The relative path to evaluate.
    /// </param>
    /// <returns>The newly calculated resource path.</returns>
    private string CalculateResourcePath(string relativePath)
    {
        var path = new StringBuilder(256);
        if (relativePath.StartsWith("..")) // back level navigation
        {
            var pathElements = ResourcePath.Split(PathSeparatorChars);
            var upWalks = UpWalks(relativePath);
            if (upWalks > pathElements.Length) throw new UriFormatException("Too many back levels.");
            var separator = PathSeparatorChars[0];
            for (var i = 0; i < pathElements.Length - upWalks; i++) path.Append(pathElements[i]).Append(separator);
            var relativeParts = relativePath.Split('/', '\\');
            for (var i = upWalks; i < relativeParts.Length - 1; i++) path.Append(relativeParts[i]).Append(separator);
            if (path.Length > 0) path.Length -= 1;
            return path.ToString();
        }

        if (relativePath.StartsWith("/")) // relative to root 
        {
            if (relativePath.Length > 1)
                return relativePath.Substring(1, relativePath.Length - 2);
            return null;
        }

        // relative to current namespace...
        if (ResourcePath != null && ResourcePath != string.Empty)
            path.Append(ResourcePath.TrimEnd(PathSeparatorChars)).Append(PathSeparatorChars[0]);
        if (relativePath.StartsWith("./"))
        {
            if (relativePath.Length > 2) path.Append(relativePath.Substring(2, relativePath.Length - 3));
        }
        else
        {
            path.Append(relativePath.Substring(0, relativePath.Length - 1));
        }

        return path.ToString();
    }

    private int UpWalks(string path)
    {
        var parts = path.Split('/', '\\');
        var count = 0;
        for (; count < parts.Length && parts[count].Equals(".."); count++) ;
        return count;
    }

    #endregion
}