﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using TLC.Data;

namespace TLC.API.ActionFIlters
{
    public class AuthorizationRequiredAttribute: ActionFilterAttribute
    {
        private const string Token = "Token";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //  Get API key provider
            var provider = new TokenRepository();

            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();

                // Validate Token
                if (provider != null && !provider.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                    filterContext.Response = responseMessage;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}