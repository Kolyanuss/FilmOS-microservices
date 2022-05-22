using System.Collections.Generic;

namespace Filmos_Rating_CleanArchitecture.Application.User.Queries.GetUsersList
{
    public class UsersListVm
    {
        public IList<UsersLookupDto> Users { get; set; }
    }
}
