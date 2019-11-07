using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace AppHub
{
    /// <summary>
    /// Represents an abstract BaseHub with all User relevant connection Methods
    /// </summary>
    /// <typeparam name="T">
    /// Hub class.
    /// </typeparam>
    public abstract class AppHub<T> : Hub<T> where T : class, IAppClient
    {
        private readonly ILogger _logger;

        public AppHub()
        {
        }

        /// <summary>
        /// Defines what happens when frontend user connects.
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.Clients.Caller.LogClient("Hi you are connected on " + typeof(T).Name);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Defines what happens when frontend user disconnects
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
