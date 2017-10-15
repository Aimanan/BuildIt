using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using Buildit.Services.Contracts;
using Bytes2you.Validation;

namespace CourseProject.Web.Hubs
{
    public class Chat : Hub
    {
        private readonly IUsersService usersService;

        public Chat(IUsersService usersService)
        {
            Guard.WhenArgument(usersService, "UsersService").IsNull().Throw();

            this.usersService = usersService;
        }

        public void SendMessage(string username, string message)
        {
            var callerName = this.Context.User.Identity.GetUserName();
            message = HttpUtility.HtmlEncode(message);
            var receiverName = $"{username}_{callerName}";
            Clients.Group(receiverName).addMessage(callerName, message);
        }

        //public void Test( string msg )
        //{
        //    string chatMsg = $"Message from {this.Context.ConnectionId}:{msg}";
        //    this.Clients.All.receiveMessage(chatMsg);
        //}


        //public void SendMessage(string message)
        //{
        //    var msg = string.Format("{0}: {1}", Context.ConnectionId, message);
        //    Clients.All.addMessage(msg);
        //}

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                Clients.Group(rooms[i]).addMessage(msg);
            }
        }

        //public void SendMessageToRoom(string message, string[] rooms, string username)
        //{
        //    var msg = string.Format("{0}: {1}", Context.ConnectionId, message);
        //    var callerName = this.Context.User.Identity.GetUserName();
        //    var receiverGroupName = $"{username}_{callerName}";

        //    for (int i = 0; i < rooms.Length; i++)
        //    {
        //        Clients.Group(rooms[i]).addMessage(username, msg);
        //    }
        //}

        public void Connect(string username)
        {
            string name = this.Context.User.Identity.GetUserName();
            var groupName = $"{name}_{username}";
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void Disconnect(string username)
        {
            string name = this.Context.User.Identity.GetUserName();
            var groupName = $"{name}_{username}";
            Groups.Remove(Context.ConnectionId, groupName);
        }

        public void CheckAndConnect(string username)
        {
            var exists = this.usersService.CheckIfUserExists(username);

            if (exists)
            {
                this.Connect(username);
                Clients.Caller.chatWith(username);
            }
            else
            {
                Clients.Caller.showError(username);
            }
        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Caller.joinRoom(room);
        }     
    }
}