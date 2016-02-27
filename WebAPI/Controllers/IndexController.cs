using System.Linq;
using System.Web.Http;
using WebAPI.ActionFilters;
using WebAPI.Messages;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class IndexController:ApiController
    {
        [RequireToken]
        public MyMessage Get()
        {
            AuthModel authModel = new AuthModel();

            var header = Request.Headers.SingleOrDefault(x => x.Key == "token");
            bool isAdmin = authModel.VerifyAdminToken(header.Value.First());
            RoleMessage msg;

            if (isAdmin)
            {
                msg = new RoleMessage("admin");
                return msg;
            }
            else
            {
                msg = new RoleMessage("user");
                return msg;
            }
            
        }
    }
}