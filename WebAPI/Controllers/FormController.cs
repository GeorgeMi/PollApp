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
    public class FormController : ApiController
    {
        FormModel formModel = new FormModel();

        [RequireToken]
        public IEnumerable<FormDTO> Get()
        {
            List<FormDTO> list = new List<FormDTO>();
            list = formModel.GetAllForms();
            return list;
        }

        [RequireToken]
        [HttpPost]
        [ActionName("user")]
        public IEnumerable<FormDTO> User (string id)
        {
            List<FormDTO> list = new List<FormDTO>();
            list = formModel.GetUserForms(id);
            return list;
        }

        [RequireToken]
        public FormDetailDTO Get(int id)
        {
           return formModel.GetContentForm(id);
        }

        [RequireToken]
        public HttpResponseMessage Post([FromBody] FormDetailDTO formDTO)
        {
            HttpResponseMessage responseMessage;
  
            bool response = formModel.AddForm(formDTO);
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

        [RequireAdminTokenOrUsername]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage responseMessage;

            bool response = formModel.DeleteForm(id);
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
