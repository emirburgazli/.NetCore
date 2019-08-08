using FirstProject.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [Route("api/category")]
    public class CategoryController : Controller
    {
        ICategoryDal _categoryDal;
        public CategoryController(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var category = _categoryDal.GetList();
            return Ok(category);
        }

        [HttpGet("{categoryId}")]
        public IActionResult Get(int categoryId)
        {
            try
            {
                var category = _categoryDal.Get(p => p.CategoryId == categoryId);

                if (category == null)
                {
                    return NotFound($"This is No Category with ID={categoryId}");
                }
                return Ok(category);
            }
            catch
            {

            }
            return BadRequest();
        }
    }
}
