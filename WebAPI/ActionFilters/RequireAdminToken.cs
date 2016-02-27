using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Models;

namespace WebAPI.ActionFilters
{
    public class RequireAdminToken : ActionFilterAttribute
    {
         /// <summary>
        /// Public default Constructor
        /// </summary>
        public override void OnActionExecuting(HttpActionContext context)
        {
            AuthModel authModel = new AuthModel();

            var header = context.Request.Headers.SingleOrDefault(x => x.Key == "token");

            bool valid, isAdmin, okDate;

            if (header.Value == null)
            {
                valid = false;
            }
            else
            {
                //tokenul apartine unui admin
                isAdmin = authModel.VerifyAdminToken(header.Value.First());

                //tokenul este valid
                okDate = authModel.VerifyToken(header.Value.First());

                valid = isAdmin && okDate;
            }

            if (!valid)
            {
                //Invalid Authorization Key
                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

        }
    }
}