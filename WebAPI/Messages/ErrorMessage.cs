using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Messages
{
    public class ErrorMessage:MyMessage
    {
        public string error;

        public ErrorMessage(string error)
        {
            this.error = error;
        }

    }
}