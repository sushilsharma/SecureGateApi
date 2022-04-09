using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.WebApi.SignalRHub.DTO
{
    public class Message
    {
        public string Type { get; set; }
        public string Information { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { set; get; }


    }
}
