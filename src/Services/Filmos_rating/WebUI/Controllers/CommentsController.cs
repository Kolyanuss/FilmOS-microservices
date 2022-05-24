using Filmos_Rating_CleanArchitecture.Application.Comment.Commands.DeleteComment;
using Filmos_Rating_CleanArchitecture.Application.Comment.Commands.UpsertComment;
using Filmos_Rating_CleanArchitecture.Application.Comment.Queries;
using Filmos_Rating_CleanArchitecture.Application.Comment.Queries.GetCommentById;
using Filmos_Rating_CleanArchitecture.Application.Comment.Queries.GetCommentList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.WebUI.Controllers
{
    public class CommentsController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCommentListQuery()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDto>> GetById(string? id)
        {
            return Ok(await Mediator.Send(new GetCommentQuery() { Id_comment_to_find = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Insert(InsertCommentCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(UpdateCommentCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            await Mediator.Send(new DeleteCommentCommand { Id = id });
            return NoContent();
        }
    }
}
