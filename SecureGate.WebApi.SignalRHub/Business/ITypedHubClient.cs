using SecureGate.WebApi.SignalRHub.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.WebApi.SignalRHub.Business
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(Message message);
    }
}
