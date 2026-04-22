using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace AYMDating.Blazor.Pages
{
    //public partial class Chat : IAsyncDisposable
    public partial class Chat : IDisposable
    {
        private HubConnection hubConnection;
        private string user;
        private string userTo;
        private DateTime DateOfMaking;
        private string message;
        private List<Message> messages = new List<Message>();

        [Inject]
        private NavigationManager Navigation { get; set; }

        //[Inject]
        //private IJSRuntime JSRuntime { get; set; }


        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }

        public class Message
        {
            public string User { get; set; }
            public string UserTo { get; set; }
            public DateTime DateOfMaking { get; set; }
            public string message { get; set; }
        }

    }
}
