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
    [Route("api/films")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public FilmsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // GET: api/Films
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Result = await _serviceManager.FilmsService.GetAll();
                return Ok(Result);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Films/5
        [HttpGet("{id}", Name = "FilmById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Result = await _serviceManager.FilmsService.GetById(id);
                return Ok(Result);
            }
            catch (FilmsNotFoundException)
            {
                return NotFound("No item found with index " + id);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Spec/{id}", Name = "FilmByIdSpec")]
        public async Task<IActionResult> GetByIdSpec(int id)
        {
            try
            {
                var Result = await _serviceManager.FilmsService.GetByIdSpec(id);
                return Ok(Result);
            }
            catch (FilmsNotFoundException)
            {
                return NotFound("No item found with index " + id);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Films/5
        [HttpGet("{id}/desc")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithDetailsById(int id)
        {
            try
            {
                var Result = await _serviceManager.FilmsService.GetWithDetailsById(id);
                return Ok(Result);
            }
            catch (FilmsNotFoundException)
            {
                return NotFound("No item found with index " + id);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Films
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] FilmsForCreationDto filmsDto)
        {
            try
            {
                var filmsDtoPrint = await _serviceManager.FilmsService.Post(filmsDto);
                return CreatedAtRoute(
                      "FilmById",
                      new { Id = filmsDtoPrint.Id },
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

        // PUT: api/Films/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] FilmsForCreationDto filmsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _serviceManager.FilmsService.Put(id, filmsDto);
                return NoContent();
            }
            catch (FilmsNotFoundException)
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

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceManager.FilmsService.Delete(id);
                return NoContent();
            }
            catch (FilmsNotFoundException)
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
