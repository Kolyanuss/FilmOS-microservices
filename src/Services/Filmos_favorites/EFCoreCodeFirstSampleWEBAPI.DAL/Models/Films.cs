using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Models
{
    public class Films
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NameFilm { get; set; }
        public DateTime ReleaseData { get; set; }
        public string Country { get; set; }
        public int FKDescriptionId { get; set; }
        [ForeignKey("FKDescriptionId")]
        public Description Description { get; set; }
        public List<FilmsUsers> FilmsUsers { get; set; }
        public List<FilmsGenres> FilmsGenres { get; set; }

        public Films()
        {
            FilmsUsers = new List<FilmsUsers>();
            FilmsGenres = new List<FilmsGenres>();
        }
    }
}
