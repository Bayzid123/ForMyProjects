using ApiForMyProjects.DTO;
using ApiForMyProjects.IRepository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiForMyProjects.Controllers
{
    [Route("ApiForMyProjects/[controller]")]
    [ApiController]
    public class UserController : ResponseHandler
    {
        private readonly IUser _IRepository;

        public UserController(IUser IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpPost]
        [Route("CreateUser")]
        [SwaggerOperation(Description = "Example {DTO}")]
        public async Task<IActionResult> CreateUser(CreateUserDTO objCreate)
        {
            try
            {
                return Ok(await _IRepository.CreateUser(objCreate));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
