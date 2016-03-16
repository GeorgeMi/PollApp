using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.ActionFilters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VoteController : ApiController
    {
        FormModel formModel = new FormModel();

        [RequireToken]
        public VoteResultDTO Post([FromBody] VoteListDTO voteDTO)
        {
            string token = Request.Headers.SingleOrDefault(x => x.Key == "token").Value.First();

            VoteResultDTO result = formModel.Vote(voteDTO,token);
            return result;
        }

    }
}