using Bookstore.Model;
using Bookstore.Model.DTO.Category;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController :ControllerBase
    {
        readonly ICategory _categoryDb;
       
        public CategoriesController(ICategory _categoryDb)
        {
            this._categoryDb = _categoryDb;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreate newCategory)
        {
            BookCategory category = newCategory.Adapt<BookCategory>();
            await _categoryDb.AddCategory(category);
            return Ok("Successful");
        } 

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId,CategoryUpdate updatedCategory)
        {
            BookCategory category = updatedCategory.Adapt<BookCategory>();
            BookCategory currentCategory = await _categoryDb.GetCategory(categoryId);
            if(currentCategory == null)
            {
                return NotFound("Not found");
            }
            await _categoryDb.UpdateCategory(category);
            return Ok("Successsful");
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            BookCategory currentCategory = await _categoryDb.GetCategory(categoryId);
            if (currentCategory == null)
            {
                return NotFound("Not found");
            }
            await _categoryDb.DeleteCategoryById(categoryId);
            return Ok("Successful");

        }

    }
}
