using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AuthController : ApiController
    {
        public AuthModel auth = new AuthModel();

      
        public string Post(UserDTO user)
        {
            return auth.Authenticate(user.Username, user.Password);
        }
        
    }
}
