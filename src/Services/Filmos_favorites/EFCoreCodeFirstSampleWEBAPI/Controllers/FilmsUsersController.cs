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
    [Route("api/filmsusers")]
    [ApiController]
    public class FilmsUsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public FilmsUsersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // GET: api/FilmsUsers
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.Get();
                return Ok(Result);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        // GET: api/FilmsUsers/5/4
        [HttpGet("{id1}/{id2}", Name = "FilmUserById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id1, int id2)
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.GetById(id1, id2);
                return Ok(Result);
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with pair index " + id1 + ":" + id2);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        #region explicit loading
        // GET: api/FilmsUsers/5/4
        [HttpGet("{id1}/{id2}/info")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdWithDetails(int id1, int id2)
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.GetByIdWithDetails(id1, id2);
                return Ok(Result);
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with pair index " + id1 + ":" + id2);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        // GET: api/FilmsUsers/filmsbyuser/5
        [HttpGet("filmsbyuser/{id1}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFilmsByUserId(int id1)
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.GetFilmsByUserId(id1);
                return Ok(Result);
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with index " + id1);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        #region explicit loading
        // GET: api/FilmsUsers/filmsbyuser/5/info
        [HttpGet("filmsbyuser/{id1}/info")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFilmsByUserIdDetails(int id1)
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.GetFilmsByUserIdDetails(id1);
                return Ok(Result);
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with index " + id1);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        // GET: api/FilmsUsers/usersbyfilm/5
        [HttpGet("usersbyfilm/{id1}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersByFilmId(int id1)
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.GetUsersByFilmId(id1);
                return Ok(Result);
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with index " + id1);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        #region explicit loading
        // GET: api/FilmsUsers/usersbyfilm/5/info
        [HttpGet("usersbyfilm/{id1}/info")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersByFilmIdDetails(int id1)
        {
            try
            {
                var Result = await _serviceManager.FilmsUsersService.GetUsersByFilmIdDetails(id1);
                return Ok(Result);
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with index " + id1);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        // POST: api/FilmsUsers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] FilmsUsersDTO filmsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var filmsDtoPrint = await _serviceManager.FilmsUsersService.Post(filmsDto);
                return CreatedAtRoute(
                      "FilmUserById",
                      new { Id1 = filmsDtoPrint.IdFilms, Id2 = filmsDtoPrint.IdUser },
                      filmsDtoPrint);
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

        // PUT: api/FilmsUsers/5/5
        [HttpPut("{id1}/{id2}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id1, int id2, [FromBody] FilmsUsersDTO clearDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _serviceManager.FilmsUsersService.Put(id1, id2, clearDto);
                return NoContent();
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with pair index " + id1 + ":" + id2);
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

        // DELETE: api/FilmsUsers/5
        [HttpDelete("{id1}/{id2}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id1, int id2)
        {
            try
            {
                await _serviceManager.FilmsUsersService.Delete(id1, id2);
                return NoContent();
            }
            catch (FilmsUsersNotFoundException)
            {
                return NotFound("No item found with pair index " + id1 + ":" + id2);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
