using Microsoft.AspNetCore.Mvc;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.EntitiesDTO;
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
                var Result = await _sqlBasketFilmsService.GetBasketByIdUser(Id);
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetByName")]
        [HttpGet]
        public async Task<IActionResult> GetBasketFilmsJoinUser()
        {
            try
            {
                var Result = await _sqlBasketFilmsService.GetBasketFilmsJoinUser();
                return Ok(Result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SQLBasketFilmsDTO BasketFilmDTO)
        {
            try
            {
                var filmsID = await _sqlBasketFilmsService.AddBasketFilm(BasketFilmDTO);
                if (filmsID.Item1 == BasketFilmDTO.id_film && filmsID.Item2 == BasketFilmDTO.id_user)
                {
                    return Ok(BasketFilmDTO);
                }
                return Accepted();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Route("AllBy/{idUser}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(long idUser)
        {
            try
            {
                await _sqlBasketFilmsService.DeleteBasketFilm(idUser);
                return Accepted();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{idFilm}/{idUser}")]
        public async Task<IActionResult> Delete(long idFilm, long idUser)
        {
            try
            {
                await _sqlBasketFilmsService.DeleteBasketFilm(idFilm, idUser);
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
