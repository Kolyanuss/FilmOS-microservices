using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // GET: api/Users
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Result = await _serviceManager.UsersService.Get();
                return Ok(Result);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "UserById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Result = await _serviceManager.UsersService.GetById(id);
                return Ok(Result);

            }
            catch (UsersNotFoundException)
            {
                return NotFound("No item found with index " + id);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] UserForCreationDto userdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userDtoPrint = await _serviceManager.UsersService.Post(userdto);
                return CreatedAtRoute(
                      "UserById",
                      new { Id = userDtoPrint.Id },
                      userDtoPrint);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Data);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UserForCreationDto userdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _serviceManager.UsersService.Put(id, userdto);
                return NoContent();
            }
            catch (UsersNotFoundException)
            {
                return NotFound("No item found with index " + id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Data);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceManager.UsersService.Delete(id);
                return NoContent();
            }
            catch (UsersNotFoundException)
            {
                return NotFound("No item found with index " + id);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
