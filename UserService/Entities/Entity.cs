namespace UserService.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a base DB Entity.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Gets/sets ID of the Entity.
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// Gets/sets Name of the Entity.
        /// </summary>
        public string Name { get; set; }
    }
}
