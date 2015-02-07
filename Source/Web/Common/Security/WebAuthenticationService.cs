﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Web;
using System.Web.Security;

using BL.Services.Security;

namespace Web.Common.Security
{
    public class WebAuthenticationService : IAuthenticateService
    {
        public int? CurrentUserId
        {
            get 
            {
                return GetCurrentUserId();
            }
        }

        public WebAuthenticationService()
        {

        }

        public void Authenticate(int userId, bool persist)
        {
            FormsAuthentication.SetAuthCookie(userId.ToString(), persist);
        }

        public void DeAuthenticate()
        {
            FormsAuthentication.SignOut();
        }

        private int? GetCurrentUserId()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                return null;
            }

            int id = 0;
            if (int.TryParse(HttpContext.Current.User.Identity.Name, out id) == false)
            {
                return null;
            }

            return id;
        }
    }
}
