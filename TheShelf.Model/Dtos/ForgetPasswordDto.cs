using System.ComponentModel.DataAnnotations;

namespace TheShelf.Model.Dtos
{
    public class ForgetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
