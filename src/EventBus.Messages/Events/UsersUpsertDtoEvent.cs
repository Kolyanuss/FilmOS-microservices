namespace EventBus.Messages.Events
{
    public class UsersUpsertDtoEvent : IntegrationBaseEvent
    {
        public int Id_User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool _is_add { get; set; }
    }
}
