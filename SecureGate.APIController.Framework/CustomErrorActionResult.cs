using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecureGate.APIController.Framework
{
    public class CustomErrorActionResult:IActionResult
    {
        private readonly string _message;
        private readonly HttpStatusCode _statusCode;

        public CustomErrorActionResult(HttpStatusCode statusCode, string statusDescription)
        {
            _statusCode = statusCode;
            _message = statusDescription;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            HttpResponseMessage response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_message)
            };

            return Task.FromResult(response);
        }
    }
}
