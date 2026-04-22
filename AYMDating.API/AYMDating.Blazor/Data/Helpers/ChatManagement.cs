using AYMDating.API.DTO;
using AYMDating.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AYMDating.Blazor.Data.Helpers
{
    public class ChatManagement
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ApiServiceLogin _ApiServiceLogin;
        private readonly AppState _appState;

        public ChatManagement(IHubContext<ChatHub> hubContext, ApiServiceLogin _ApiServiceLogin, AppState _appState)
        {
            _hubContext = hubContext;
            this._ApiServiceLogin = _ApiServiceLogin;
            _appState = this._appState;
        }

        public async Task AddToGroup(string connectionId, string groupName)
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task SendMessage(string groupName, string user, string user2, DateTime dateOfMaking, string message)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", user, user2, dateOfMaking, message);
        }
    }
}
