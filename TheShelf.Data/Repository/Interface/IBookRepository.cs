using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;

namespace TheShelf.Data.Repository.Interface
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(string id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task CreateBookAsync(CreateBookDto createBookDto);
        Task UpdateBookAsync(string id, UpdateBookDto updateBookDto);
        Task DeleteBookAsync(string id);
        Task UpdatePhotoAsync(string id, string imageUrl);
        Task<IEnumerable<BookDto>> GetBooksByISBNAsync(string isbn);
        IEnumerable<BookDto> GetBooksByTitle(string title);
        IEnumerable<BookDto> GetBooksByAuthor(string author);
        IEnumerable<BookDto> GetBooksByPublisher(string publisher);
        Task<IEnumerable<BookDto>> GetBooksByYearPublishedAsync(int year);
    }
}
