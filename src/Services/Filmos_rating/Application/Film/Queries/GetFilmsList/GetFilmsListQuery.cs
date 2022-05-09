using MediatR;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList
{
    public class GetFilmsListQuery : IRequest<FilmsListVm>
    {
    }
}
