using System.Collections.Generic;
using System.Threading.Tasks;
using AppHub;

namespace ChatHub
{
    public class ChatHub : AppHub<IChatClient>
    {
        public async Task BroadcastMessage(string message)
        {
            await this.Clients.All.SendMessage(message);
        }
    }

    public interface IChatClient : IAppClient
    {
        Task SendMessage(string message);
    }
}
