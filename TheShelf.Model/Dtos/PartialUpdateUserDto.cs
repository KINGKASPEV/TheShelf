using System.ComponentModel.DataAnnotations;

namespace TheShelf.Model.Dtos
{
    public class PartialUpdateUserDto
    {
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
