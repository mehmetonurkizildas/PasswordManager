using Application.Services.Abstract;
using Application.Services.Repositories;
using AutoMapper;
using Core.Exceptions;
using Domain.Dtos.Account;
using Domain.Entities;

namespace Application.Services.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountManager(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto model)
        {
            var user = await _accountRepository.Get(x => x.Email == model.Email);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new BusinessException("Username or password is incorrect");

            //update last login data
            user.LastLoginDate = DateTime.Now;
            await _accountRepository.Update(user);


            // authentication successful
            var response = _mapper.Map<UserLoginResponseDto>(user);
            return new UserLoginResponseDto
            {
                AuthenticateResult = true,
                FullName = user.FirstName + " " + user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                AccountType = user.AccountType,
                Id = user.Id,
            };
        }

        public async Task<UserRegisterResponseDto> RegisterUsersAsync(UserRegisterRequestDto model)
        {
            //kullanıcı kayitlimi kontrolu
            if (await _accountRepository.Get(x => x.Email == model.Email) != null)
            {
                return null;
            }

            var user = _mapper.Map<Account>(model);
            user.Phone = "05391111111";
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _accountRepository.Add(user);

            return _mapper.Map<UserRegisterResponseDto>(user);
        }
    }
}
