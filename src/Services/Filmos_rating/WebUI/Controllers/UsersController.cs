using Filmos_Rating_CleanArchitecture.Application.User.Queries.GetUsersList;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.DeleteUsers;
using Filmos_Rating_CleanArchitecture.Application.User.Commands.UpsertUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.WebUI.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsersListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUsersListQuery()));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsersListVm>> GetAllAdmin()
        {
            return Ok(await Mediator.Send(new GetUsersAdminListQuery()));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsersListVm>> GetUserWithLongName()
        {
            return Ok(await Mediator.Send(new GetUsersListWithLongNameQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertUsersCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            await Mediator.Send(new DeleteUsersCommand { Id = id });

            return NoContent();
        }
    }
}
