using System.Collections.Generic;

namespace Filmos_Rating_CleanArchitecture.Application.Film.Queries.GetFilmsList
{
    public class FilmsListVm
    {
        public IList<FilmsLookupDto> Films { get; set; }
    }
}
