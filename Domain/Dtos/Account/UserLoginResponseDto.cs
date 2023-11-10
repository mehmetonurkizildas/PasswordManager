using Domain.Enums;

namespace Domain.Dtos.Account
{
    public class UserLoginResponseDto
    {
        public long Id { get; set; }
        public bool AuthenticateResult { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public AccountType AccountType { get; set; }
    }
}
