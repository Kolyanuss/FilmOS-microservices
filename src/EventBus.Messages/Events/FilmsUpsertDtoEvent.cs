using System;

namespace EventBus.Messages.Events
{
    public class FilmsUpsertDtoEvent : IntegrationBaseEvent
    {
        public int Id_Film { get; set; }
        public string NameFilm { get; set; }
        public DateTime Data { get; set; }
        public string Country { get; set; }
        public int FKDescriptionId { get; set; }
        public bool _is_add { get; set; }

    }
}
