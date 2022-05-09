using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Models
{
    public class Genres
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
        public List<FilmsGenres> FilmsGenres { get; set; }
    }
}
