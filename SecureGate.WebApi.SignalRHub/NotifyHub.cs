using SecureGate.WebApi.SignalRHub.Business;
using SecureGate.WebApi.SignalRHub.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.WebApi.SignalRHub
{
    public class UserConnection
    {
        public string UserName { set; get; }
        public string ConnectionId { set; get; }

    }
    public static class UserHandler
    {
        public static List<UserConnection> uList = new List<UserConnection>();
    }
    public class NotifyHub : Hub<ITypedHubClient> 
    {

        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        public NotifyHub(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public override Task OnConnectedAsync()
        {
   
            UserConnection connection = new UserConnection();
            connection.ConnectionId = Context.ConnectionId;
            connection.UserName = Context.User.Identity.Name;
            UserHandler.uList.Add(connection);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {

            UserConnection connection = new UserConnection();
            connection.ConnectionId = Context.ConnectionId;
            connection.UserName = Context.User.Identity.Name;
            UserHandler.uList.Remove(connection);

            return base.OnDisconnectedAsync(exception);
        }

        public void UserDetailsInSignalr(Message userInfo)
        {
            dynamic objOutput = new ExpandoObject();
            string retMessage = string.Empty;
            try
            {
                List<UserConnection> userdata = UserHandler.uList.Where(p => p.ConnectionId == userInfo.ConnectionId).ToList();
                if (userdata.Count() > 0)
                {
                    userdata[0].UserName = userInfo.UserName;
                }
                objOutput.retMessage = "Success";

                //_hubContext.Clients.All.BroadcastMessage(userInfo);
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
        }


    }

}
