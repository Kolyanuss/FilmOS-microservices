using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password can't be longer than 100 symbols and minimum 5 symbols", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field \"IsAdmin\" is required")]
        public bool IsAdmin { get; set; }
    }
}
