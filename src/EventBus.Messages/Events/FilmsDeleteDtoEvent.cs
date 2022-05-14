namespace EventBus.Messages.Events
{
    public class FilmsDeleteDtoEvent : IntegrationBaseEvent
    {
        public int Id_Film { get; set; }
    }
}
