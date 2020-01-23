using Framework.Business.Response;
using Framework.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public class Destination
    {
        public string Uri { get; set; }
        public bool RequiresAuthentication { get; set; }
        public bool isAuthentication { get; set; }

        static HttpClient client = new HttpClient();

        private void SetClient()
        {
            var networkCredential = new NetworkCredential(
                "user name",
                "password",
                "domain");

            var handler = new HttpClientHandler { Credentials = networkCredential };

            client = new HttpClient(handler);
        }

        public Destination(string uri, bool requiresAuthentication)
        {
            Uri = uri;
            RequiresAuthentication = requiresAuthentication;
        }

        public Destination(string path)
            : this(path, false)
        {
        }

        private Destination()
        {
            Uri = "/";
            RequiresAuthentication = false;
        }

        private string CreateDestinationUri(HttpRequest request, string queryString = null)
        {

            if (isAuthentication)
            {
                return Uri + queryString;
            }

            queryString = request.QueryString.ToString();

            var array = request.Path.ToString().Substring(1).Split('/');
            string endpoint = "";
            for (int i = 1; i < array.Length; i++)
            {
                if (i > 1)
                {
                    endpoint += "/";
                }
                endpoint += array[i];
            }

            //Log.Instance.Function(new StringBuilder().Append(Uri + endpoint + queryString));

            return Uri + endpoint + queryString;
        }

        private HttpRequestMessage GenerateRequest(HttpContext context, string queryString = null, bool isAuth = false)
        {
            var requestMessage = new HttpRequestMessage();
            // Copy the request method
            requestMessage.Method = new HttpMethod(context.Request.Method);
            // Copy the request content
            if (!StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "GET") &&
               !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "HEAD") &&
               !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "DELETE") &&
               !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "TRACE") &&
               !isAuth)
            {
                requestMessage.Content = new StreamContent(context.Request.Body);
            }
            // Copy the request headers
            var headers = new[] { "Origin" };
            foreach (var header in context.Request.Headers)
            {
                if (!headers.Contains(header.Key))
                {
                    if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                    {
                        requestMessage.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                    }
                }
                else
                {
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, "263e51096d956c4948253b97e68a5769");
                }
            }

            // Construct the request URL
            requestMessage.RequestUri = new Uri(CreateDestinationUri(context.Request, queryString));

            //// Set host header
            //var baseAddress = dispatcher.BaseAddress;
            //requestMessage.Headers.Host = baseAddress.Host + ":" + baseAddress.Port;

            return requestMessage;
        }

        public async Task SendRequest(HttpContext context, int IdCompany, int UserId)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var requestMessage = this.GenerateRequest(context);

            requestMessage.Headers.TryAddWithoutValidation("IdCompany", IdCompany.ToString());
            requestMessage.Headers.TryAddWithoutValidation("UserId", UserId.ToString());

            var sb = new StringBuilder();
            sb.AppendLine("||||||||||||||||||||||||||");
            sb.AppendLine("Send async");
            sb.AppendLine("||||||||||||||||||||||||||");
            Log.Instance.Function(sb);
            try
            {

            using (var responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                 sb = new StringBuilder();
                sb.AppendLine("||||||||||||||||||||||||||");
                sb.AppendLine("responseMessage");
                sb.AppendLine(responseMessage.StatusCode.ToString());
                foreach (var item in responseMessage.Headers)
                {
                    sb.AppendLine(item.Key + " -- "+item.Value);
                }
                sb.AppendLine("||||||||||||||||||||||||||");
                Log.Instance.Function(sb);

                if (responseMessage.StatusCode != HttpStatusCode.OK && responseMessage.StatusCode != HttpStatusCode.NoContent)
                {
                    var ex = new Exception(responseMessage.Content.ReadAsStringAsync().Result);
                    Log.Instance.ErrorLog(ex);
                }

                // If the service is temporarily unavailable, throw to retry later.
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    responseMessage.EnsureSuccessStatusCode();
                }

                // Copy the status code
                context.Response.StatusCode = (int)responseMessage.StatusCode;

                var headers = new[] { "Access-Control-Allow-Origin" };
                // Copy the response headers
                foreach (var header in responseMessage.Headers)
                {
                    if (!headers.Contains(header.Key))
                    {
                        context.Response.Headers[header.Key] = header.Value.ToArray();
                    }
                    else
                    {
                        context.Response.Headers[header.Key] = context.Request.Headers["Origin"];
                    }
                }

                foreach (var header in responseMessage.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                // SendAsync removes chunking from the response. This removes the header so it doesn't expect a chunked response.
                context.Response.Headers.Remove("transfer-encoding");

                // Copy the response content
                if (responseMessage.StatusCode != HttpStatusCode.NoContent)
                {
                    await responseMessage.Content.CopyToAsync(context.Response.Body);
                }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorLog(ex);
                throw;
            }
        }

        public async Task<Tuple<bool, int, int>> ValidateAuthentication(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Request.Headers.TryGetValue("OperationType", out StringValues OperationType);
            var path = context.Request.Path.ToString().Substring(1);
            var operation = OperationType.ToString() == "" ? "READ" : OperationType.ToString();
            var requestMessage = this.GenerateRequest(context, "?" + "OperationType=" + operation + "&ModuleName=" + path.Split('/')[0] + "&ControllerName=" + path.Split('/')[1], true);
            requestMessage.Method = new HttpMethod("GET");

            Debug.WriteLine(DateTime.Now);
            using (var responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                Debug.WriteLine(DateTime.Now);
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(DateTime.Now);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new Tuple<bool, int, int>(false, 0, 0);
                }

                var obj = JsonConvert.DeserializeObject<ResponseResult>(JsonConvert.DeserializeObject(content).ToString());
                var contentObj = JsonConvert.DeserializeObject<UserCredentials>(obj.Data.ToString());

                return new Tuple<bool, int, int>(obj.State == ResponseState.Success, contentObj.IdCompany, contentObj.UserId);
            }
        }

    }

    public class UserCredentials
    {
        public int UserId { get; set; }

        public int IdCompany { get; set; }
    }
}
