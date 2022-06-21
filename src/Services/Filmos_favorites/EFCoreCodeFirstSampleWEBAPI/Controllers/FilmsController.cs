using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ClientIdPolicy")]
    public class FilmsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<FilmsController> _logger;

        public FilmsController(IServiceManager serviceManager, ILogger<FilmsController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Films
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.GetAll()");
                var Result = await _serviceManager.FilmsService.GetAll();
                _logger.LogInformation("Sucsefully get all film");
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Some error: ", ex.Message);
                return StatusCode(500, ex.Message);
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
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.GetById()");
                var Result = await _serviceManager.FilmsService.GetById(id);
                return Ok(Result);
            }
            catch (FilmsNotFoundException)
            {
                _logger.LogError("Film found");
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
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.GetByIdSpec()");
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

        // GET: api/Films/5/desc
        [HttpGet("{id}/desc")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithDetailsById(int id)
        {
            try
            {
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.GetWithDetailsById()");
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
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.Post()");
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
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.Put()");
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
                _logger.LogInformation("In " + this.GetType() + " call FilmsService.Delete()");
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
