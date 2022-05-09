using System;

namespace EventBus.Messages.Events
{
    public class FilmsDtoEvent : IntegrationBaseEvent
    {
        public int? Id_Film { get; set; } = null;
        public string NameFilm { get; set; }
        public DateTime Data { get; set; }
        public string Country { get; set; }
        public int FKDescriptionId { get; set; }
    }
}
