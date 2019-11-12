namespace UserService.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
