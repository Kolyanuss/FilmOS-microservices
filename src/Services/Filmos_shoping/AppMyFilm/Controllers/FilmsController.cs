using AppMyFilm.DAL.Entities.SQLEntities;
using AppMyFilm.DAL.Interfaces.SQLInterfaces.ISQLServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppMyFilm.WEBAPI.Controllers
{
    [Route("api/[controller]")]

    public class FilmsController : ControllerBase
    {
        #region Propertirs
        ISQLFilmsService _FilmsService;
        #endregion

        #region Constructors
        public FilmsController(ISQLFilmsService sqlFilmsService)
        {
            _FilmsService = sqlFilmsService;
        }
        #endregion

        #region APIs
        [Route("Films")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Result = await _FilmsService.GetAllFilms();
                return Ok(Result);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("FilmById/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Result = await _FilmsService.GetFilmById(Id);
                return Ok(Result);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("FilmsPop")]
        [HttpGet]
        public async Task<IActionResult> GetNotPopularFilms()
        {
            try
            {
                var Result = await _FilmsService.GetNotPopularFilms();
                return Ok(Result);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("Films")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SQLFilmsDTO filmsDto)
        {
            try
            {
                var filmsID = await _FilmsService.AddFilm(filmsDto);
                var FilmForPrint = await _FilmsService.GetFilmById(filmsID);
                return CreatedAtRoute(
                      "FilmById",
                      new { Id = FilmForPrint.Id },
                      FilmForPrint);
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("Films/{id?}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] SQLFilmsDTO filmsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _FilmsService.UpdateFilm(id, filmsDto);
                return NoContent();
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("Films/{id?}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _FilmsService.DeleteFilm(id);
                return NoContent();
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion
    }
}
