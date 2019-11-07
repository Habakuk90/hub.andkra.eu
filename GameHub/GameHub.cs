namespace GameHub
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppHub;

    public class GameHub : AppHub<IGameClient>
    {
        public GameHub()
        {
           
        }

        public async Task<string> BroadcastMessage(string message)
        {
            await this.Clients.All.SendMessage(this.Context.ConnectionId);

            await this.Clients.All.SendMessage("Hi from GameHub");

            return message;
        }
    }
    public interface IGameClient : IAppClient
    {
        Task SendMessage(string message);
    }
}
