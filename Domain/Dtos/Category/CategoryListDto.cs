namespace Domain.Dtos.Category
{
    public class CategoryListDto
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required int PasswordCount { get; set; }
    }
}
