
using Application.Services.Repositories;
using Core.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class AccountRepository : EfRepositoryBase<Account, PasswordManagerDbContext>, IAccountRepository
    {
        public AccountRepository(PasswordManagerDbContext context) : base(context)
        {

        }
    }
}
