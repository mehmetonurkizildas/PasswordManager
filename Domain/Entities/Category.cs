using Core.Repositories;

namespace Domain.Entities
{

    public class Category : Entity
    {
        public required string Name { get; set; }
        public long AccountId { get; set; }
        public required Account Account { get; set; }
        public ICollection<PasswordCollection> PasswordCollections { get; set; } = new List<PasswordCollection>();
  
    }
}
