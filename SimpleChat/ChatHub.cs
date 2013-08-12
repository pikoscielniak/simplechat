using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SimpleChat
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = string.Format("{0} {1}", Context.ConnectionId, message);
            Clients.All.newMessage(msg);
        }

        public void SendMessageData(SendData data)
        {
            Clients.All.newData(data);
        }

        //        public Task<int> SendDataAsync()
        //        {
        //            throw new NotImplementedException();
        //        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageFromRoom(string room, string message)
        {
            var msg = string.Format("{0} {1}", Context.ConnectionId, message);
            Clients.Group(room).newMessage(msg);
        }

        public override Task OnDisconnected()
        {
            SendMonitoringData("Disconnected", Context.ConnectionId);
            return base.OnDisconnected();
        }

        public override Task OnConnected()
        {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            SendMonitoringData("Reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void SendMonitoringData(string eventType, string connectionId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitoringHub>();
            context.Clients.All.newEvent(eventType, connectionId);
        }
    }

    public class SendData
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}