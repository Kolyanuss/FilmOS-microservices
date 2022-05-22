using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmos_Rating_CleanArchitecture.Application.Common.Exceptions
{
    class MissedValueException : Exception
    {
        public MissedValueException(string entity, string val)
            : base($"In Entity \"{entity}\" missed value ({val}).")
        {
        }
    }
}
