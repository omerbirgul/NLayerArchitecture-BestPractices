using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Categories;
using Services.Categories.Dtos.Create;
using Services.Categories.Dtos.Update;

namespace App.API.Controllers
{

    public class CategoriesController(ICategoryService _categoryService) : CustomBaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await _categoryService.GetAllAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResult = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts()
        {
            var serviceResult = await _categoryService.GetCategoryWithProductsAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:int}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            var serviceResult = await _categoryService.GetCategoryWithProductsAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var serviceResult = await _categoryService.CreateAsync(request);
            return CreateActionResult(serviceResult);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequest request)
        {
            var serviceResult = await _categoryService.UpdateAsync(id, request);
            return CreateActionResult(serviceResult);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _categoryService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }
    }
}
