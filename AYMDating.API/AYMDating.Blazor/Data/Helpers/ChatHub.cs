using Microsoft.AspNetCore.SignalR;

namespace AYMDating.Blazor.Data.Helpers
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user,"General", DateTime.Now, message);
            //await Clients.All.SendAsync("ReceiveMessage", user,user2,DateOfMaking, message);
        }

        //public async Task AddToGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //}

        //public async Task SendMessageToGroup(string groupName, string user, string user2, DateTime dateOfMaking, string message)
        //{
        //    await Clients.Group(groupName).SendAsync("ReceiveMessage", user, user2, dateOfMaking, message);
        //}
    }
}
