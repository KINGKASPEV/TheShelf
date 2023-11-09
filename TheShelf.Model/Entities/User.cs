using Microsoft.AspNetCore.Identity;

namespace TheShelf.Model.Entities
{
    public class User: IdentityUser
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Book> Books { get; set; }
    }
}
