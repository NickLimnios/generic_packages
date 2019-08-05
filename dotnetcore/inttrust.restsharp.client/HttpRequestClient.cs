using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace inttrust.restsharp.client
{
    public static class HttpRequestClient
    {
        public static IRestResponse ExecuteRestGet(string path, Dictionary<string, string> headers = null)
        {
            return ExecuteRestRequest(path, Method.GET, headers: headers);
        }

        public static IRestResponse ExecuteRestPost(string path, string jsonBody, Dictionary<string, string> headers = null, Parameter parameter = null)
        {
            return ExecuteRestRequest(path, Method.POST, jsonBody, headers, parameter);
        }

        public static IRestResponse ExecuteRestPut(string path, string jsonBody, Dictionary<string, string> headers = null)
        {
            return ExecuteRestRequest(path, Method.PUT, jsonBody, headers);
        }


        public static IRestResponse ExecuteRestDelete(string path, string jsonBody = null, Dictionary<string, string> headers = null)
        {
            return ExecuteRestRequest(path, Method.DELETE, jsonBody, headers: headers);
        }

        public static T GetValue<T>(this IRestResponse restResponse, string responseField)
        {
            return JObject.Parse(JsonConvert.DeserializeObject(restResponse.Content).ToString()).Value<T>(responseField);
        }

        private static IRestResponse ExecuteRestRequest(string path, Method method, string jsonBody = null, Dictionary<string, string> headers = null, Parameter parameter = null)
        {
            ExecuteRequestSettings(path);

            var client = new RestClient(path);
            var request = new RestRequest(method);

            if (headers != null) request = FillHeaders(request, headers);

            if (jsonBody.Clear() != null)
            {
                request.AddParameter("application/json", System.Text.Encoding.UTF8.GetBytes(jsonBody),
                    ParameterType.RequestBody);
            }

            if (parameter != null)
                request.AddParameter(parameter);

            return client.Execute(request);
        }

        private static void ExecuteRequestSettings(string path)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
        }

        private static RestRequest FillHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            return request;
        }

        private static string Clear(this string value)
        {
            if (value == null)
                return null;
            if (value.Trim() == "")
                return null;
            return value.Trim();
        }
    }
}
