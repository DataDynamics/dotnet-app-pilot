using DataDynamics.Common.Spring.Resources;
using DataDynamics.Common.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace DataDynamics.App.Tests;

public class FileTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FileMetadata()
    {
        IResourceLoader loader = new ConfigurableResourceLoader();
        var resource = loader.GetResource("Tests/config.yml");
        Console.Out.WriteLine($"File : {resource.File}");
        Console.Out.WriteLine($"Description : {resource.Description}");
        Console.Out.WriteLine($"Exists : {resource.Exists}");
        Console.Out.WriteLine($"IS Open : {resource.IsOpen}");
        Console.Out.WriteLine($"URI : {resource.Uri}");

        Console.Out.WriteLine($"Name : {resource.File.Name}");
        Console.Out.WriteLine($"Exists : {resource.File.Exists}");
        Console.Out.WriteLine($"Directory : {resource.File.Directory}");
        Console.Out.WriteLine($"Directory Name : {resource.File.DirectoryName}");
        Console.Out.WriteLine($"Length : {resource.File.Length}");
        Console.Out.WriteLine($"IsReadOnly : {resource.File.IsReadOnly}");
        Console.Out.WriteLine($"CreationTime : {resource.File.CreationTime}");
        Console.Out.WriteLine($"Extension : {resource.File.Extension}");
        Console.Out.WriteLine($"FullName : {resource.File.FullName}");
        Console.Out.WriteLine($"Attributes : {resource.File.Attributes}");
    }
}