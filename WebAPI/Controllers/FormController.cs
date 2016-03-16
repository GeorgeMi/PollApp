using DataTransferObject;
using System.Collections.Generic;
using System.Linq;
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
            string token = Request.Headers.SingleOrDefault(x => x.Key == "token").Value.First();
           
            List<FormDTO> list = new List<FormDTO>();
            list = formModel.GetAllForms(token);
            return list;
        }

        [RequireToken]
        [HttpPost]
        [ActionName("user")]
        public IEnumerable<FormDTO> User(string id)
        {
            List<FormDTO> list = new List<FormDTO>();
            list = formModel.GetUserForms(id);
            return list;
        }

        [RequireToken]
        [HttpGet]
        [ActionName("result")]
        public VoteResultDetailDTO Result(int id) //returneaza rezultatul unui sondaj propriu
        {
            VoteResultDetailDTO voteResult = formModel.GetDetailResultForm(id);
            return voteResult;
        }

        [RequireToken]
        [HttpGet]
        [ActionName("getForm")]
        public FormDetailDTO GetForm(int id)
        {
           return formModel.GetContentForm(id);
        }

        [RequireToken]
        [HttpGet]
        [ActionName("voted")]
        public IEnumerable<FormDTO> Voted(string id) //returneaza lista sondajelor votate de catre un user
        {
            List<FormDTO> list = new List<FormDTO>();
            list = formModel.GetVotedForms(id);
            return list;
        }

        [RequireToken]
        [HttpGet]
        [ActionName("category")]
        public IEnumerable<FormDTO> Category(int id) //returneaza lista sondajelor dintr-o categorie
        {
            List<FormDTO> list = new List<FormDTO>();
            string token = Request.Headers.SingleOrDefault(x => x.Key == "token").Value.First();

            list = formModel.GetCategoryForms(id, token);
            return list;
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
