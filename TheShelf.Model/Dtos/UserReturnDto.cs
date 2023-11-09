namespace TheShelf.Model.Dtos
{
    public class UserReturnDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Errors { get; set; }
        public string ErrorMessage { get; set; }
    }
}
