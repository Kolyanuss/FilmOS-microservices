using Microsoft.AspNetCore.Mvc;
using Shoping.DAL.Entities.SQLEntities;
using Shoping.DAL.Interfaces.SQLInterfaces.ISQLServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMyFilm.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    public class BasketFilmsController
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
        [Route("BasketFilms")]
        [HttpGet]
        public IAsyncEnumerable<SQLBasketFilms> Get()
        {
            return _sqlBasketFilmsService.GetAllBasketFilms();
        }

        [Route("BasketFilmsFilm/{Id}")]
        [HttpGet]
        public IAsyncEnumerable<SQLBasketFilms> GetByFilm(long Id)
        {
            return _sqlBasketFilmsService.GetBasketFilmByIdFilm(Id);
        }

        [Route("BasketFilmsUser/{Id}")]
        [HttpGet]
        public IAsyncEnumerable<SQLBasketFilms> GetByUser(long Id)
        {
            return _sqlBasketFilmsService.GetBasketFilmByIdUser(Id);
        }

        /*[Route("BasketFilmsJoin")]
        [HttpGet]
        public IAsyncEnumerable<SQLBasketFilmsStr> GetBasketFilmsJoinUser()
        {
            return _sqlBasketFilmsService.GetBasketFilmsJoinUser();
        }*/

        [Route("BasketFilms")]
        [HttpPost]
        public Task<long> Post([FromBody] SQLBasketFilms BasketFilm)
        {
            return _sqlBasketFilmsService.AddBasketFilm(BasketFilm);
        }

        [Route("BasketFilms/{id?}")]
        [HttpPut]
        public void Put([FromBody] SQLBasketFilms basketFilm)
        {
            _sqlBasketFilmsService.UpdateBasketFilm(basketFilm);
        }

        [Route("BasketFilms/{id?}")]
        [HttpDelete]
        public void Delete([FromBody] SQLBasketFilms basketFilm)
        {
            _sqlBasketFilmsService.DeleteBasketFilm(basketFilm);
        }
        #endregion
    }
}
