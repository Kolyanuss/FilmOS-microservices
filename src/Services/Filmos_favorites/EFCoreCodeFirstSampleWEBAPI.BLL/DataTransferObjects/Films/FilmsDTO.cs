using System;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects
{
    public class FilmsDTO
    {
        public int Id { get; set; }
        public string NameFilm { get; set; }
        public DateTime Data { get; set; }
        public string Country { get; set; }
        public int FKDescriptionId { get; set; }
    }
}
