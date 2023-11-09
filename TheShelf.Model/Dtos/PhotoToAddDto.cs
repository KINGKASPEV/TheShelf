using Microsoft.AspNetCore.Http;

namespace TheShelf.Model.Dtos
{
    public class PhotoToAddDto
    {
        public IFormFile PhotoFile { get; set; }

    }
}
