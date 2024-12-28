using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Products;
using Services.Products.Dtos.Requests;

namespace App.API.Controllers
{
    public class ProductsController(IProductService _productService) : CustomBaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await _productService.GetAllAsync();
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResult = await _productService.GetById(id);
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPageAll(int pageNumber, int pageSize)
        {
            var serviceResult = await _productService.GetPagedListAsync(pageNumber, pageSize);
            return CreateActionResult(serviceResult);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var serviceResult = await _productService.CreateAsync(request);
            return CreateActionResult(serviceResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            var serviceResult = await _productService.UpdateAsync(id, request);
            return CreateActionResult(serviceResult);
        }

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
        {
            var serviceResult = await _productService.UpdateStockAsync(request);
            return CreateActionResult(serviceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await _productService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }
    }
}
