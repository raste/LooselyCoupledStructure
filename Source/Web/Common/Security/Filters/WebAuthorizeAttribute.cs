﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Linq;
using System.Web.Mvc;

using BL.Services.Accounts;

namespace Web.Controllers
{
    /// <summary>
    /// Main authorization attribute, use where access has to be restricted
    /// </summary>
    public class WebAuthorizeAttribute : FilterAttribute
    {
        public Role[] Roles = default(Role[]);

        public WebAuthorizeAttribute(params Role[] roles)
        {
            this.Roles = roles;
        }
    }

    /// <summary>
    /// Contains the autohirazation logic
    /// <para> This filter is being automatically applied where WebAuthorizeAttribute is used </para>
    /// </summary>
    public class WebAuthorizeFilter : IAuthorizationFilter
    {
        private IAccountService accountsService = null;

        private Role[] roles = default(Role[]);

        public WebAuthorizeFilter(IAccountService accountsService, params Role[] roles)
        {
            this.accountsService = accountsService;
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (roles.Count() == 0 && accountsService.CurrentUser != null)
            {
                return;
            }

            if (accountsService.CurrentUser != null
                && roles.Contains(accountsService.CurrentUser.Role) == true)
            {
                return;
            }

            filterContext.Result = new RedirectResult("/Home/Index");
        }
    }


}