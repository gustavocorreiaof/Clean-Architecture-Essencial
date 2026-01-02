using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryDTO categoryDto)
        {
            Category entity = _mapper.Map<Category>(categoryDto);
            await _repository.CreateAsync(entity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> entities =  await _repository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(entities);
        }

        public async Task<CategoryDTO> GetByIdAsync(Guid id)
        {
            Category entity  = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task RemoveCategoryAsync(Guid id)
        {
            Category entity = _repository.GetByIdAsync(id).Result;
            await _repository.RemoveAsync(entity);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            Category entity = _mapper.Map<Category>(categoryDto);
            await _repository.UpdateAsync(entity);
        }
    }
}
