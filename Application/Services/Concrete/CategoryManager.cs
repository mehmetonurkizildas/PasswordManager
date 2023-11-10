using Application.Services.Abstract;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Dtos.Category;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<List<CategoryListDto>> CategoryList(long accountId)
        {
            var data = await _categoryRepository.GetList(x => x.AccountId == accountId && x.IsActive == true, include: x => x.Include(a => a.PasswordCollections));
            return _mapper.Map<List<CategoryListDto>>(data);
        }

        public async Task<CategoryDto> GetCategory(long accountId, long? id)
        {
            var category = await _categoryRepository.Get(x => x.AccountId == accountId && x.Id == id);
            if (category != null)
            {
                return _mapper.Map<CategoryDto>(category);
            }
            return new CategoryDto();
        }

        public async Task<CategoryDto?> CategoryAddOrUpdate(long accountId, CategoryDto categoryDto)
        {
            if (categoryDto.Id > 0)
            {
                var category = await _categoryRepository.Get(x => x.Id == categoryDto.Id && x.AccountId == accountId);
                if (category == null)
                {
                    return null;
                }

                category = _mapper.Map<Category>(categoryDto);
                category.AccountId = accountId;
                var updatedResult = await _categoryRepository.Update(category);
                if (updatedResult != null)
                {
                    return categoryDto;
                }
            }
            else
            {
                var addedCategory = _mapper.Map<Category>(categoryDto);
                addedCategory.AccountId = accountId;
                var addResult = await _categoryRepository.Add(addedCategory);
                if (addResult != null)
                {
                    return categoryDto;
                }
            }
            return null;
        }

        public async Task<CategoryDto?> DeleteCategory(long accountId, long id)
        {
            var category = await _categoryRepository.Get(x => x.Id == id && x.AccountId == accountId);
            if (category == null)
            {
                return null;
            }
            else
            {
                category.IsActive = false;
                var updatedResult = await _categoryRepository.Update(category);
                if (updatedResult != null)
                {
                    return _mapper.Map<CategoryDto>(updatedResult);
                }
            }
            return null;
        }
    }
}
