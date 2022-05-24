using System.Collections.Generic;

namespace Filmos_Rating_CleanArchitecture.Application.Comment.Queries.GetCommentList
{
    public class CommentListVm
    {
        public IList<CommentDto> CommentsList { get; set; }
    }
}
