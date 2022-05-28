using Microsoft.AspNetCore.Mvc;
using Shoping.DAL.EntitiesDTO;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Result = await _FilmsService.GetAllFilms();
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Result = await _FilmsService.GetFilmById(Id);
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("NotPopular")]
        [HttpGet]
        public async Task<IActionResult> GetNotPopularFilms()
        {
            try
            {
                var Result = await _FilmsService.GetNotPopularFilms();
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SQLFilmsForAddDTO filmsDto)
        {
            try
            {
                var filmsID = await _FilmsService.AddFilm(filmsDto);
                /*var FilmForPrint = await _FilmsService.GetFilmById(filmsID);
                return CreatedAtRoute(
                      "FilmById",
                      new { Id = FilmForPrint.Id },
                      FilmForPrint);*/
                return Accepted();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SQLFilmsForAddDTO filmsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _FilmsService.UpdateFilm(id, filmsDto);
                return Accepted();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _FilmsService.DeleteFilm(id);
                return Accepted();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
