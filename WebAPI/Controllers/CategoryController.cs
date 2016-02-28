using DataTransferObject;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.ActionFilters;
using WebAPI.Messages;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        CategoryModel categoryModel = new CategoryModel();

        [RequireToken]
        public IEnumerable<CategoryDTO> Get()
        {
           return categoryModel.GetAllCategories();
              
        }

        [RequireAdminToken]
        public HttpResponseMessage Post(CategoryDTO categoryDTO)
        {
            HttpResponseMessage responseMessage;

            bool response = categoryModel.AddCategory(categoryDTO);
            if (response)
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }

            return responseMessage;

        }

        [RequireAdminToken]
        public HttpResponseMessage Delete (int id)
        {
            HttpResponseMessage responseMessage;

            bool response = categoryModel.DeleteCategory(id);
            if (response)
            {
                SuccessMessage msg = new SuccessMessage("deleted");
                responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }

            return responseMessage;

        }

    }
}
