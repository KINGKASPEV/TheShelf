using TheShelf.Model.Enums;

namespace TheShelf.Model.Entities
{
    public class Book
    {
        public string Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int YearPublished { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateAdded { get; set; }
        public string ImageUrl { get; set; }

        public BookCategory Category { get; set; }
        public Genre Genre { get; set; }
        public string AddedById { get; set; }
        public User AddedBy { get; set; }
    }
}
