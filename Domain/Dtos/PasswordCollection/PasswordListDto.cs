namespace Domain.Dtos.PasswordCollection
{
    public class PasswordListDto
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Url { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
