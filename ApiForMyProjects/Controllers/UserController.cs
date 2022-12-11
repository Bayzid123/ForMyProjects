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


        #region AdPlay Written Test
        [HttpPost]
        [Route("SaveTrack")]
        [SwaggerOperation(Description = "Example {DTO}")]
        public async Task<IActionResult> SaveTrack(SaveTrackDTO obj)
        {
            try
            {
                return Ok(await _IRepository.SaveTrack(obj));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTrackList")]
        [SwaggerOperation(Description = "Example {DTO}")]
        public async Task<IActionResult> GetTrackList(string search)
        {
            try
            {
                return Ok(await _IRepository.GetTrackList(search));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("DeleteTrack")]
        [SwaggerOperation(Description = "Example {DTO}")]
        public async Task<IActionResult> DeleteTrack(long id)
        {
            try
            {
                return Ok(await _IRepository.DeleteTrack(id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
