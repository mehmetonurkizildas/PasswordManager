using Domain.Dtos.Account;

namespace Application.Services.Abstract
{
    public interface IAccountService
    {
        Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto model);
        Task<UserRegisterResponseDto> RegisterUsersAsync(UserRegisterRequestDto request);
    }
}
