namespace EventBus.Messages.Events
{
    public class UsersDtoEvent : IntegrationBaseEvent
    {
        public int? Id_User { get; set; } = null;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
