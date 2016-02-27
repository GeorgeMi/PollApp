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

        
        public IEnumerable<FormDTO> Get()
        {
            List<FormDTO> list = new List<FormDTO>();
            list = formModel.GetAllForms();
            return list;
        }

        public FormDetailDTO Get(int id)
        {
           return formModel.GetContentForm(id);
        }

        public HttpResponseMessage Post(FormDetailDTO formDTO)
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
    }
}
