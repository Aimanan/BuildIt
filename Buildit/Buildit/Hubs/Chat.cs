//using Microsoft.AspNet.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Buildit.Hubs
//{
//    public class Chat : Hub
//    {
//        private readonly ILogger logger;

//        public Chat(ILogger logger)
//        {
//            this.logger = logger;
//        }
//        public void SendMessage(string message)
//        {
//            string chatMessage = $"Message form{this.Context.ConnectionId} : {message}";
//            this.Clients.All.receiveMessage(chatMessage);
//        }
//    }
//}