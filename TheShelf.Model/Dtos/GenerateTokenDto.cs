using System.ComponentModel.DataAnnotations;

namespace TheShelf.Model.Dtos
{
    public class GenerateTokenDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
