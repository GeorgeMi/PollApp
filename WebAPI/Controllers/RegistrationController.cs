using DataTransferObject;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Messages;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class RegistrationController : ApiController
    {
         public HttpResponseMessage Post(UserRegistrationDTO user)
        {
            UsersModel userModel = new UsersModel();
            bool add = userModel.AddUser(user);
            HttpResponseMessage response;
            

            if (add)
            {
                SuccessMessage msg = new SuccessMessage("Registration successful!");
                
                response = Request.CreateResponse(HttpStatusCode.OK, msg);
                return response;

            }
            else
            {
                ErrorMessage msg = new ErrorMessage("Registration failed! Username or email already exists");

                response = Request.CreateResponse(HttpStatusCode.Forbidden, msg);
                return response;

            }
        }



    }
}
