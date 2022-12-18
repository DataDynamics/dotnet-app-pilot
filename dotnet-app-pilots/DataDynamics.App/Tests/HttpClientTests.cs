using DataDynamics.Common.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace DataDynamics.App.Tests;

public class HttpClientTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetValue()
    {
        var options = new RestClientOptions("https://www.naver.com")
        {
            ThrowOnAnyError = true,
            Timeout = 1000
        };

        var parameters = DictionaryUtils.Create("A", "B");


        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        

        var client = new RestClient(options);
    }
}