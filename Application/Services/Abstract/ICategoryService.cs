using Domain.Dtos.Category;

namespace Application.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListDto>> CategoryList(long accountId);
        Task<CategoryDto> GetCategory(long accountId, long? id);
        Task<CategoryDto?> CategoryAddOrUpdate(long accountId, CategoryDto categoryDto);
        Task<CategoryDto?> DeleteCategory(long accountId, long id);
    }
}
