using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TheShelf.Data.DbContext;
using TheShelf.Data.Repository.Interface;
using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;

namespace TheShelf.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly TheShelfDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(TheShelfDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _context.Books
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(string id, UpdateBookDto updateBookDto)
        {
            var book = await GetBookByIdAsync(id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _mapper.Map(updateBookDto, book);

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(string id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhotoAsync(string id, string imageUrl)
        {
            var foundBook = await _context.Books.FindAsync(id);
            if (foundBook == null)
            {
                throw new Exception("Book not found");
            }

            foundBook.ImageUrl = imageUrl;

            _context.Update(foundBook);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            return await _context.Books
                .FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByISBNAsync(string isbn)
        {
            var normalizedISBN = isbn.ToUpper();

            var books = await _context.Books
                .Where(book => book.Isbn.ToUpper() == normalizedISBN)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return books;
        }

        public IEnumerable<BookDto> GetBooksByTitle(string title)
        {
            return _context.Books
                .AsEnumerable()
                .Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Select(book => _mapper.Map<BookDto>(book))
                .ToList();
        }

        public IEnumerable<BookDto> GetBooksByAuthor(string author)
        {
            return _context.Books
                .AsEnumerable()
                .Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                .Select(book => _mapper.Map<BookDto>(book))
                .ToList();
        }


        public IEnumerable<BookDto> GetBooksByPublisher(string publisher)
        {
            return _context.Books
                .AsEnumerable()
                .Where(book => book.Publisher.Contains(publisher, StringComparison.OrdinalIgnoreCase))
                .Select(book => _mapper.Map<BookDto>(book))
                .ToList();
        }


        public async Task<IEnumerable<BookDto>> GetBooksByYearPublishedAsync(int year)
        {
            return await _context.Books
                .Where(book => book.YearPublished == year)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}

