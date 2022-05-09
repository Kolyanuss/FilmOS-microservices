using System;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects
{
    public class FilmsDetailDTO
    {
        public int Id { get; set; }
        public string NameFilm { get; set; }
        public DateTime ReleaseData { get; set; }
        public string Country { get; set; }
        public DescriptionDTO Description { get; set; }
    }
}
