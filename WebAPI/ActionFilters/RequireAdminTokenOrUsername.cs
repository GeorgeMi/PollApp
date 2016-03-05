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
    public class RequireAdminTokenOrUsername : ActionFilterAttribute
    {
        /// <summary>
        /// Public default Constructor
        /// </summary>
        public override void OnActionExecuting(HttpActionContext context)
        {
            AuthModel authModel = new AuthModel();
            FormModel formModel = new FormModel();

            var header = context.Request.Headers.SingleOrDefault(x => x.Key == "token");
            var formIdToDelete = context.Request.RequestUri.Segments[3];

            bool valid=false, isAdmin=false, okDate=false, formIsFromUser=false;

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

                //tokenul si formul apartin aceluiasi user
                formIsFromUser = formModel.FormIdCreatedbyUserId(Int32.Parse(formIdToDelete), header.Value.First());

            }

            if (!(valid || formIsFromUser))
            {
                //Invalid Authorization Key
                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

        }
    }
}