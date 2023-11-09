using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using TheShelf.Model.Dtos;

namespace TheShelf.MVC.Controllers
{
	public class BookController : Controller
	{
		private readonly HttpClient _httpClient;

		public BookController(IConfiguration configuration)
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri(configuration.GetSection("ApiSettings:BaseUrl").Value);
		}


		[HttpGet]
		public async Task<IActionResult> BookList()
		{
			List<BookDto> books = new List<BookDto>();

			HttpResponseMessage response = await _httpClient.GetAsync("api/book/list-of-books");

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				books = JsonConvert.DeserializeObject<List<BookDto>>(data);
			}

			return View(books);
		}

		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			try
			{
				BookDto bookDto = new BookDto();
				HttpResponseMessage response = await _httpClient.GetAsync($"api/book/id?id={id}");

				if (response.IsSuccessStatusCode)
				{
					var data = await response.Content.ReadAsStringAsync();
					bookDto = JsonConvert.DeserializeObject<BookDto>(data);

					if (bookDto != null)
					{
						return View(bookDto);
					}
				}
				return NotFound();
			}
			catch (Exception)
			{
				TempData["errorMessage"] = "An error occurred while retrieving the book details.";
				return RedirectToAction("BookList");
			}
		}


		[HttpGet]
		public async Task<IActionResult> Search(string searchCriteria)
		{
			try
			{
				List<BookDto> searchResult = new List<BookDto>();
				HttpResponseMessage response = await _httpClient.GetAsync($"api/book/search?searchCriteria={searchCriteria}");

				if (response.IsSuccessStatusCode)
				{
					var data = await response.Content.ReadAsStringAsync();
					searchResult = JsonConvert.DeserializeObject<List<BookDto>>(data);

					if (searchResult != null && searchResult.Count > 0)
					{
						return View(searchResult);
					}
				}

				// If no books found or an error occurred, return a view with a message.
				return View("NoResults");
			}
			catch (Exception)
			{
				TempData["errorMessage"] = "An error occurred while searching for books.";
				return RedirectToAction("BookList");
			}
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(CreateBookDto book)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var response = await _httpClient.PostAsJsonAsync("api/book/add-book", book);

					if (response.IsSuccessStatusCode)
					{
						TempData["successMessage"] = "Book created successfully";
						return RedirectToAction("BookList");
					}
					else
					{
						TempData["errorMessage"] = "An error occurred while creating the book.";
					}
				}
				catch (Exception ex)
				{
					TempData["errorMessage"] = "An error occurred while creating the book: " + ex.Message;
				}
			}
			else
			{
				TempData["errorMessage"] = "Invalid data. Please check the form for errors.";
			}
			return View(book);
		}


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                UpdateBookDto updateBookDto = new UpdateBookDto();
                HttpResponseMessage response = await _httpClient.GetAsync($"api/book/id?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    updateBookDto = JsonConvert.DeserializeObject<UpdateBookDto>(data);

                    if (updateBookDto != null)
                    {
                        TempData["successMessage"] = "Book details retrieved successfully";
                        return View(updateBookDto);
                    }
                }

                TempData["errorMessage"] = "Book not found or an error occurred while retrieving the data.";
                return RedirectToAction("BookList");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while retrieving the book data: " + ex.Message;
                return RedirectToAction("BookList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBookDto updateBookDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"api/book/update/{updateBookDto.Id}", updateBookDto);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Book details updated successfully";
                        return RedirectToAction("BookList");
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        TempData["errorMessage"] = "Book not found";
                    }
                    else
                    {
                        TempData["errorMessage"] = "An error occurred while updating the book details.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = "An error occurred while updating the book details: " + ex.Message;
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data. Please check the form for errors.");
                return View(updateBookDto);
            }
            return View(updateBookDto);
        }


        [HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				BookDto bookDto = new BookDto();
				HttpResponseMessage response = await _httpClient.GetAsync($"api/book/id?id={id}");

				if (response.IsSuccessStatusCode)
				{
					var data = await response.Content.ReadAsStringAsync();
					bookDto = JsonConvert.DeserializeObject<BookDto>(data);

					if (bookDto != null)
					{
						return View(bookDto);
					}
				}
				return NotFound();
			}
			catch (Exception)
			{
				TempData["errorMessage"] = "An error occurred while retrieving the book data.";
				return RedirectToAction("BookList");
			}
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"api/book/delete/{id}");

				if (response.IsSuccessStatusCode)
				{
					TempData["successMessage"] = "Book deleted successfully";
					return RedirectToAction("BookList");
				}
				else if (response.StatusCode == HttpStatusCode.NotFound)
				{
					return NotFound("Book not found");
				}
				else
				{
					return View("Error");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet]
		public IActionResult UpdateImage(string id)
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateImage(string id, [FromForm] IFormFile photoFile)
		{
			try
			{
				var multipartContent = new MultipartFormDataContent();
				multipartContent.Add(new StreamContent(photoFile.OpenReadStream())
				{
					Headers =
			{
				ContentLength = photoFile.Length,
				ContentType = new MediaTypeHeaderValue(photoFile.ContentType)
			}
				}, "photoFile", photoFile.FileName);

				var response = await _httpClient.PatchAsync($"api/book/photo/{id}", multipartContent);

				if (response.IsSuccessStatusCode)
				{
					var imageUrl = await response.Content.ReadAsStringAsync();
					return Ok(new { Url = imageUrl });
				}
				else if (response.StatusCode == HttpStatusCode.NotFound)
				{
					return NotFound("Book not found");
				}
				else
				{
					return View("Error");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

	}
}
