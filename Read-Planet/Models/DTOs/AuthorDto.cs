namespace Read_Planet.Models.DTOs
{
    public class AuthorDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
        public string ImageUrl { get; set; }

        public int NoOfBooks { get; set; }
    }
}
