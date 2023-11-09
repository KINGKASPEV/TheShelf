using System.ComponentModel.DataAnnotations;
using TheShelf.Model.Enums;

namespace TheShelf.Model.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public string Publisher { get; set; }
        [Required]
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public BookCategory Category { get; set; }
        [Required]
        public Genre Genre { get; set; }
        public string AddedById { get; set; }
    }
}
