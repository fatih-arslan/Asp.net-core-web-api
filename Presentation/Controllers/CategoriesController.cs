using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CategoriesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet] // api/categories
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            return Ok(await _serviceManager.CategoryService.GetCategoriesAsync(false));
        }

        [HttpGet("{id:int}")] // api/categories/id
        public async Task<IActionResult> GetCategoryAsync([FromRoute] int id)
        {
            var category = Ok(await _serviceManager.CategoryService.GetCategoryByIdAsync(id, false));

            if(category == null)
            {
                throw new CategoryNotFoundException(id);
            }
            return Ok(category);
        }
    }
}
