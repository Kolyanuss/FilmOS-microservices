namespace EventBus.Messages.Events
{
    public class UsersDeleteDtoEvent : IntegrationBaseEvent
    {
        public int Id_User { get; set; }
    }
}
