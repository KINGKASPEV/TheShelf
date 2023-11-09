using AutoMapper;
using Microsoft.AspNetCore.Http;
using TheShelf.Data.Repository.Interface;
using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;
using TheShelf.Service.Interface;

namespace TheShelf.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public BookService(IBookRepository bookRepository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return _mapper.Map<Book>(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<CreateBookDto>(createBookDto);
            await _bookRepository.CreateBookAsync(book);
        }

        public async Task UpdateBookAsync(string id, UpdateBookDto updateBookDto)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);
            if (existingBook != null)
            {
                _mapper.Map(updateBookDto, existingBook);
                await _bookRepository.UpdateBookAsync(id, updateBookDto);
            }
        }

        public async Task DeleteBookAsync(string id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<string> UpdatePhotoAsync(string id, IFormFile photoFile)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return null;
            }

            if (photoFile == null || photoFile.Length <= 0)
            {
                return null;
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(photoFile);

            if (string.IsNullOrEmpty(imageUrl))
            {
                return null;
            }

            await _bookRepository.UpdatePhotoAsync(id, imageUrl);

            return imageUrl;
        }
        public async Task<IEnumerable<BookDto>> GetBooksByISBNAsync(string isbn)
        {
            return await _bookRepository.GetBooksByISBNAsync(isbn);
        }

        public IEnumerable<BookDto> GetBooksByTitle(string title)
        {
            return _bookRepository.GetBooksByTitle(title);
        }

        public IEnumerable<BookDto> GetBooksByAuthor(string author)
        {
            return _bookRepository.GetBooksByAuthor(author);
        }

        public IEnumerable<BookDto> GetBooksByPublisher(string publisher)
        {
            return _bookRepository.GetBooksByPublisher(publisher);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByYearPublishedAsync(int year)
        {
            return await _bookRepository.GetBooksByYearPublishedAsync(year);
        }

    }
}
