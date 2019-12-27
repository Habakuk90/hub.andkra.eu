namespace UserService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The Entity Manager performs CRUD operations for the DB.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Base Entity which are stored in DB
    /// </typeparam>
    public class EntityManager<TEntity> : IEntityManager<TEntity> where TEntity : Entity
    {
        public readonly AppDbContext _context;

        /// <summary>
        /// Constructor of the EntityManager
        /// </summary>
        /// <param name="context">
        /// <see cref="DbContext"/> for the crud operations.
        /// </param>
        public EntityManager(AppDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Gets <see cref="Entity"/> by given argument
        /// </summary>
        /// <param name="item">
        /// Given <see cref="Entity"/> to be searched by.
        /// </param>
        /// <returns>
        /// The DB entry of given <see cref="TEntity"/> or null
        /// </returns>
        public virtual async Task<TEntity> Get(TEntity item)
        {
            TEntity entity = null;

            if (item.ID != Guid.Empty)
            {
                entity = await this._context.Set<TEntity>().FindAsync(item.ID);
            }
            else if (!string.IsNullOrWhiteSpace(item.Name))
            {
                entity = await this._context.Set<TEntity>().FirstOrDefaultAsync(x => x.Name == item.Name);
            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var entity = await this._context.Set<TEntity>().ToListAsync();

            return entity ?? new List<TEntity>();
        }

        /// <summary>
        /// Adds or updates given <see cref="Entity"/>
        /// </summary>
        /// <param name="item">
        /// <see cref="Entity"/> which should be added or updated.
        /// </param>
        /// <returns>
        /// Asynchronous Task.
        /// </returns>
        public virtual async Task<TEntity> AddOrUpdate(TEntity item)
        {
            try
            {
                if (await this.ItemExists(item))
                {
                    _context.Set<TEntity>().Update(item);
                }
                else
                {
                    _context.Set<TEntity>().Add(item);
                }

                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Adds or updates given <see cref="IList{T}"/> with <see cref="Entity"/>
        /// </summary>
        /// <param name="items">
        /// List of items which should be updated
        /// </param>
        /// <returns>
        /// <see cref="Task"/>
        /// </returns>
        public virtual async Task AddOrUpdate(IList<TEntity> items)
        {
            foreach (var item in items)
            {
                await this.AddOrUpdate(item);
            }
        }

        /// <summary>
        /// Removes the given item from the Database.
        /// </summary>
        /// <param name="item">
        /// <see cref="Entity"/> which should be removed.
        /// </param>
        /// <returns>
        /// <see cref="Tasks"/>
        /// </returns>
        public virtual async Task Remove(TEntity item)
        {
            try
            {
                _context.Set<TEntity>().Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Check if there is an entry in database.
        /// </summary>
        /// <param name="item">
        /// <see cref="Entity"/> which should be checked.
        /// </param>
        /// <returns></returns>
        private async Task<bool> ItemExists(TEntity item)
        {
            bool itemExists = false;

            itemExists = await _context.Set<TEntity>().AnyAsync(i => i.ID == item.ID);

            return itemExists;
        }
    }
}
