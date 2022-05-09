namespace EFCoreCodeFirstSampleWEBAPI.DAL.Models
{
    public class FilmsGenres
    {
        public int IdFilms { get; set; }
        public Films Films { get; set; }

        public int IdGenres { get; set; }
        public Genres Genres { get; set; }

    }
}
