namespace EFCoreCodeFirstSampleWEBAPI.DAL.Models
{
    public class FilmsUsers
    {
        public int IdFilms { get; set; }
        public Films Films { get; set; }

        public int IdUser { get; set; }
        public User User { get; set; }
    }
}
