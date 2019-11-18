namespace UserService.Entities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserService.Models;

    public interface IEntityManager<T> where T : Entity
    {
        Task<T> Get(T item);

        Task<IEnumerable<T>> GetAll();

        Task AddOrUpdate(T item);

        Task AddOrUpdate(IList<T> items);

        Task Remove(T item);
    }
}
