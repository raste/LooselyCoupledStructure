﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Web.Mvc;

using BL.Services;
using BL.Entities;
using BL.Common.Attributes;

namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// It is public for property injection
        /// <para> Do not call it outside of BaseController </para>
        /// </summary>
        [DCInject]
        public IServicesHolder SS { get; set; }

        public Account CurrentUser
        {
            get
            {
                return SS.AccountService.CurrentUser;
            }
        }

        public BaseController()
        {

        }

    }
}
