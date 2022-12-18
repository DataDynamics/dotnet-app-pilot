using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;

namespace DataDynamics.Common.Utils;

public class HttpUtils
{
    public static HttpClient HttpClient(string url)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(url);
        return client;
    }

    public static void AddQueryParameters(RestRequest request, Dictionary<string, string> parameters)
    {
        foreach (var parameter in parameters)
        {
            request.AddQueryParameter(parameter.Key, parameter.Value);
        }
    }

    public static void AddParameters(RestRequest request, Dictionary<string, string> parameters)
    {
        foreach (var parameter in parameters)
        {
            request.AddParameter(parameter.Key, parameter.Value);
        }
    }

    public static RestClient RestClient(string url, bool throwOnAnyError = true, int timeout = 1000)
    {
        var options = new RestClientOptions(url)
        {
            ThrowOnAnyError = throwOnAnyError,
            Timeout = timeout
        };
        return new RestClient(options);
    }

    public static RestRequest GetJsonRequest()
    {
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        return request;
    }
    public static RestRequest GetJsonRequest(string json)
    {
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody(json);
        return request;
    }

}