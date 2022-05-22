using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Models
{
    public class Description
    {
        public int Id { get; set; }
        [Required]
        public string DescriptionText { get; set; }
        public string Author { get; set; }
    }
}
