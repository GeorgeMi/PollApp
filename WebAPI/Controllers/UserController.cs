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
    public class UserController : ApiController
    {
        UsersModel userModel = new UsersModel();

        // GET api/User
        [RequireAdminToken]
        public IEnumerable<UserDetailDTO> Get()
        {
            List<UserDetailDTO> list = new List<UserDetailDTO>();
            list = userModel.GetAllUsers();
            return list;
        }

        [RequireToken]
        public UserDetailDTO Get(int id)
        {
            return userModel.GetUser(id);
        }

        [RequireAdminToken]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage responseMessage;

            bool response = userModel.Delete(id);
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

        [RequireAdminToken]
        [HttpGet]
        [ActionName("promote")]
        public HttpResponseMessage Promote(int id)
        {
                       
            HttpResponseMessage responseMessage;

            bool response = userModel.PromoteUser(id);
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
        [HttpGet]
        [ActionName("demote")]
        public HttpResponseMessage Demote(int id)
        {

            HttpResponseMessage responseMessage;

            bool response = userModel.DemoteUser(id);
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

        [RequireToken]
        [ActionName("aaa")]
        public string GetUser(int id, string s)
        {
            return id.ToString() + " asdasdasdasds";
        }
    }
}
