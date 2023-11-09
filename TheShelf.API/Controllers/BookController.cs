using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheShelf.Model.Dtos;
using TheShelf.Service.Interface;

namespace TheShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBookById(string id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("list-of-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(
      [FromQuery] string? searchCriteria)
        {
            if (string.IsNullOrWhiteSpace(searchCriteria))
            {
                return BadRequest("No search criteria provided.");
            }

            if (searchCriteria.Length >= 5 && long.TryParse(searchCriteria, out _))
            {
                var isbn = searchCriteria;
                var books = await _bookService.GetBooksByISBNAsync(isbn);
                if (books.Any())
                {
                    return Ok(new { SearchCriteria = $"ISBN: {isbn}", Books = books });
                }
            }
            else if (int.TryParse(searchCriteria, out int year))
            {
                var books = await _bookService.GetBooksByYearPublishedAsync(year);
                if (books.Any())
                {
                    return Ok(new { SearchCriteria = $"Year Published: {year}", Books = books });
                }
            }
            else
            {
                var titleBooks = _bookService.GetBooksByTitle(searchCriteria);
                var authorBooks = _bookService.GetBooksByAuthor(searchCriteria);
                var publisherBooks = _bookService.GetBooksByPublisher(searchCriteria);

                var books = titleBooks.Concat(authorBooks).Concat(publisherBooks).Distinct().ToList();

                if (books.Any())
                {
                    return Ok(new { SearchCriteria = searchCriteria, Books = books });
                }
            }

            return NotFound($"No books found with the provided search criteria: {searchCriteria}");
        }


        [HttpPost("add-book")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            await _bookService.CreateBookAsync(createBookDto);
            return CreatedAtAction("GetBookById", new { id = createBookDto.Id }, createBookDto);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (id != updateBookDto.Id)
            {
                return BadRequest("The provided ID does not match the book's ID.");
            }

            var existingBook = await _bookService.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            await _bookService.UpdateBookAsync(id, updateBookDto);

            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }


        [HttpPatch("photo/{id}")]
        public async Task<IActionResult> UpdateImage(string id, [FromForm] PhotoToAddDto model)
        {
            try
            {
                var imageUrl = await _bookService.UpdatePhotoAsync(id, model.PhotoFile);

                if (imageUrl == null)
                {
                    return NotFound("Book not found");
                }

                return Ok(new { Url = imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}

