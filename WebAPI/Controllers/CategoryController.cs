using DataTransferObject;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.ActionFilters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        CategoryModel categoryModel = new CategoryModel();

        public IEnumerable<CategoryDTO> Get()
        {
            return categoryModel.GetAllCategories();        
        }

    }
}
