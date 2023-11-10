using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Account
{
    public class AccountDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public decimal NotificationRate { get; set; }
    }
}
