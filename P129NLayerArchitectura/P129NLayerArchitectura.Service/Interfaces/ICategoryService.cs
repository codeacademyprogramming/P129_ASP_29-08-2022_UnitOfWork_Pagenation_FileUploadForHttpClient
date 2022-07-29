using P129NLayerArchitectura.Service.DTOS;
using P129NLayerArchitectura.Service.DTOS.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P129NLayerArchitectura.Service.Interfaces
{
    public interface ICategoryService
    {
        Task PostAsync(CategoryPostDto categoryPostDto);
        Task<PagenatedListDto<CategoryListDto>> GetAllAysnc(int page);
        Task<CategoryGetDto> GetById(int id);
        Task PutAsync(int id, CategoryPutDto categoryPutDto);
        Task DeleteAsync(int id);
    }
}
