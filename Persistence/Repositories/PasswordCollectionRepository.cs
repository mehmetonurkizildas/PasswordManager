using Application.Services.Repositories;
using Core.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class PasswordCollectionRepository : EfRepositoryBase<PasswordCollection, PasswordManagerDbContext>, IPasswordCollectionRepository
    {
        public PasswordCollectionRepository(PasswordManagerDbContext context) : base(context)
        {
            
        }
    }
}
