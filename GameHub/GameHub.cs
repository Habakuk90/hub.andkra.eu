namespace GameHub
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppHub;

    public class GameHub : AppHub<IGameClient>
    {
        public IGameClient GameClient { get; private set; }


        public GameHub()
        {
           
        }

        public async Task<string> BroadcastMessage(string message)
        {
            await this.Clients.All.SendMessage(this.Context.ConnectionId);

            await this.Clients.All.SendMessage("Hi from Chathub");

            return message;
        }

        //public override Task AddCurrentUser(string userName, bool isAnonymous = true)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public override Task<string> JoinGroup(string groupName)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public override Task<string> LeaveGroup(string groupName)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
    public interface IGameClient : IAppClient
    {
        //Task Hello(string message);
        //Task SwitchTurn();

        //Task TileChange(string tileId);

        //Task GameOver(string winningTileId, string winningLine);
        Task SendMessage(string message);
    }
}
