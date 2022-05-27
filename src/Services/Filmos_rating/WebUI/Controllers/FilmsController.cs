using Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList;
using Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.DeleteFilms;
using Filmos_Rating_CleanArchitecture.Application.Film.Commands.UpsertFilms;

namespace Filmos_Rating_CleanArchitecture.WebUI.Controllers
{
    public class FilmsController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilmsListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetFilmsListQuery()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilmsLookupDto>> GetById(string? id)
        {
            return Ok(await Mediator.Send(new GetFilmQuery() { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Insert(InsertFilmCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(UpdateFilmCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            await Mediator.Send(new DeleteFilmsCommand { Id = id });
            return NoContent();
        }
    }
}
