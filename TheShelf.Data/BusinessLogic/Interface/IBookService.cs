using Microsoft.AspNetCore.Http;
using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;

namespace TheShelf.Service.Interface
{
    public interface IBookService
    {
        Task<Book> GetBookByIdAsync(string id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task CreateBookAsync(CreateBookDto createBookDto);
        Task UpdateBookAsync(string id, UpdateBookDto updateBookDto);
        Task DeleteBookAsync(string id);
        Task<string> UpdatePhotoAsync(string id, IFormFile photoFile);
        Task<IEnumerable<BookDto>> GetBooksByISBNAsync(string isbn);
        IEnumerable<BookDto> GetBooksByTitle(string title);
        IEnumerable<BookDto> GetBooksByAuthor(string author);
        IEnumerable<BookDto> GetBooksByPublisher(string publisher);
        Task<IEnumerable<BookDto>> GetBooksByYearPublishedAsync(int year);

    }
}
