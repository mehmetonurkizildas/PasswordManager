using Core.Repositories;
using Domain.Enums;

namespace Domain.Entities
{

    public class Account : Entity, ICloneable
    {

        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public AccountType AccountType { get; set; } = AccountType.BasicAccount;
        public required string PasswordHash { get; set; } //Password hashli şekilde saklanıyor     
        public DateTime? LastLoginDate { get; set; }
        public required string Phone { get; set; }
        public ICollection<PasswordCollection> PasswordCollections { get; set; } = new List<PasswordCollection>();
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
