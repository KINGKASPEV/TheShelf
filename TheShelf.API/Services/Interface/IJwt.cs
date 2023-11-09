using TheShelf.Model.Dtos;
using TheShelf.Model.Entities;

namespace TheShelf.API.Services.Interface
{
    public interface IJwt
    {
        public string CreateToken(LoginDto user);
    }
}
