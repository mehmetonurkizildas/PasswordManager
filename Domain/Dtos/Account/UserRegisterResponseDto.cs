using Domain.Enums;

namespace Domain.Dtos.Account
{
    public class UserRegisterResponseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Phone { get; set; }
        public AccountType AccountType { get; set; }

    }
}
