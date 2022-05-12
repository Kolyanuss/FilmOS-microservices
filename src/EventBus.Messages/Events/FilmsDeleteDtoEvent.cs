using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class FilmsDeleteDtoEvent : IntegrationBaseEvent
    {
        public int Id_Film { get; set; }
    }
}
