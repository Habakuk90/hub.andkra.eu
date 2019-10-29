namespace AppHub
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents Base Hub methods for <see cref="Hub{T}"/>.
    /// </summary>
    public interface IAppClient
    {
        Task LogClient(string message);
    }


    public interface IAppLogger
    {
        string LogClient(string message);
    }
}
