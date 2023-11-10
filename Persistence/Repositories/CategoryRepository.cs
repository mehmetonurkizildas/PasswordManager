using Application.Services.Repositories;
using Core.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CategoryRepository :  EfRepositoryBase<Category,PasswordManagerDbContext>, ICategoryRepository
    {
        public CategoryRepository(PasswordManagerDbContext context) : base(context)
        {
            
        }
    }
}
