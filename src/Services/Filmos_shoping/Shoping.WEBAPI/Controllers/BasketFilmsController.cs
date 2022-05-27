using Microsoft.AspNetCore.Mvc;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMyFilm.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    public class BasketFilmsController : ControllerBase
    {
        #region Propertirs
        ISQLBasketFilmsService _sqlBasketFilmsService;
        #endregion

        #region Constructors
        public BasketFilmsController(ISQLBasketFilmsService sqlBasketFilmsService)
        {
            _sqlBasketFilmsService = sqlBasketFilmsService;
        }
        #endregion

        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Result = await _sqlBasketFilmsService.GetAllBasketFilms();
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("ByFilm/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetByFilm(long Id)
        {
            try
            {
                var Result = await _sqlBasketFilmsService.GetBasketByIdFilm(Id);
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("ByUser/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetByUser(long Id)
        {
            try
            {
                var Result = _sqlBasketFilmsService.GetBasketByIdUser(Id);
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /*[Route("BasketFilmsJoin")]
        [HttpGet]
        public IAsyncEnumerable<SQLBasketFilmsStr> GetBasketFilmsJoinUser()
        {
            return _sqlBasketFilmsService.GetBasketFilmsJoinUser();
        }*/

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SQLBasketFilms BasketFilm)
        {
            try
            {
                var filmsID = await _sqlBasketFilmsService.AddBasketFilm(BasketFilm);
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


        [Route("AllByUserId/{id?}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sqlBasketFilmsService.DeleteBasketFilm(id);
                return Accepted();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("idFilm/{id}/idUser/{id2}")]
        public async Task<IActionResult> Delete(int id, int id2)
        {
            try
            {
                await _sqlBasketFilmsService.DeleteBasketFilm(id, id2);
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
