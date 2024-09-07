using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace SecureGateApi
{
    public class SecureGateRequestLogger
    {

        //public GRLogger(RequestDelegate grRequest,ICacheService cacheService )
        public SecureGateRequestLogger(RequestDelegate grRequest)
        {
            GrRequest = grRequest;

            //cachServicepub =  cacheService;
        }
        public RequestDelegate GrRequest { get; }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                var builder = new StringBuilder();
                var request = await FormatRequest(context.Request);
                builder.AppendLine("Request headers:");
                List<string> lstHeaders = new List<string> { "Authorization", "User-Agent", "OS-Name", "OS-Version", "SecureGate-APP-Name", "SecureGate-APP-Version" };
                foreach (var header in context.Request.Headers)
                {
                    var matchingvalues = lstHeaders.Where(stringToCheck => stringToCheck.Contains(header.Key));
                    if (matchingvalues.Count() > 0)
                        builder.Append(header.Key).Append(':').AppendLine(header.Value);
                }
                builder.Append("Response: ").AppendLine(context.Response.StatusCode.ToString());
                LogInformation(builder.ToString());
            }
            catch (Exception ex)
            {
                LogInformation(ex.Message);
            }

            await GrRequest(context);

        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            // Do some processing with body…
            var formattedRequest = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;
            LogInformation("Request: " + formattedRequest);
            return formattedRequest;
        }
        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);
            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);
            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }

        private void LogInformation(string info)
        {
            ILogger _logger;
            _logger = LogManager.GetLogger("RequestLogger");
            _logger.Info(info);


        }

    }
}
