using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects
{
    public class FilmsForCreationDto
    {
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string NameFilm { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime Data { get; set; }

        [StringLength(50, ErrorMessage = "Name country can't be longer than 50 characters")]
        public string Country { get; set; }
        public int FKDescriptionId { get; set; }
    }
}
