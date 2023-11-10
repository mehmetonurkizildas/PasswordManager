using Core.Repositories;
using EntityFrameworkCore.EncryptColumn.Attribute;

namespace Domain.Entities
{
    public class PasswordCollection : Entity
    {
        public required long AccountId { get; set; }
        public required string Url { get; set; }
        public required long CategoryId { get; set; }
        public required string Username { get; set; }
        [EncryptColumn] public required string Password { get; set; }

        // Foreign keys
        public required Account Account { get; set; }
        public required Category Category { get; set; }
    }
}
