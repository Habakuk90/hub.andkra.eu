namespace UserService.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserService.Entities;
    using UserService.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : DatabaseController<User>
    {
        public UsersController(IEntityManager<User> manager) : base(manager)
        {
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<User>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            return await this.Get(new User { Name = name });
        }
    }
}
