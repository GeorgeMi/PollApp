using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using WebAPI.Models;

namespace WebAPI.ActionFilters
{
    public class RequireToken : ActionFilterAttribute
    {
        /// <summary>
        /// Public default Constructor
        /// </summary>
        public override void OnActionExecuting(HttpActionContext context)
        {
            AuthModel authModel = new AuthModel(); 

            var header = context.Request.Headers.SingleOrDefault(x => x.Key == "token");
                        
            bool valid;

            if (header.Value == null)
            {
               valid = false;
            }
            else
            {
                valid= authModel.VerifyToken(header.Value.First());
            }
                       
            if (!valid)
            {
               //Invalid Authorization Key
                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

        }
    }
}