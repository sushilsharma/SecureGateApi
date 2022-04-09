
using SecureGate.WebApi.SignalRHub.Business;
using SecureGate.WebApi.SignalRHub.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using SecureGate.APIController.Framework.Controllers;
using SecureGate.APIController.Framework.AppLogger;

namespace SecureGate.WebApi.SignalRHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRMessageController : BaseAPIController
    {

        protected override EnumLoggerType LoggerName
        {
            get
            {
                return EnumLoggerType.Product;
            }
        }

        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        public SignalRMessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }


        [HttpPost]
        [Route("BroadcastMessage")]
        public string BroadcastMessage(Message message)
        {
            string retMessage = string.Empty;
            try
            {

                var dd = UserHandler.uList.ToList();


                if (message.ConnectionId == null)
                {
                    _hubContext.Clients.All.BroadcastMessage(message);
                }
                else
                {
                    _hubContext.Clients.Client(message.ConnectionId).BroadcastMessage(message);
                }

                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return retMessage;
        }



    }
}
