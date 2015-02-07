﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using BL.Entities;

using BL.Common.Attributes;

namespace BL.Services
{
    /// <summary>
    /// Holds property with references to all services
    /// <para> Inherit the class if service is using other services </para>
    /// <para> This spares adding them to the constructor of the service implementation and/or to it's methods parameters </para>
    /// </summary>
    public abstract class ServicesContainer
    {
        /// <summary>
        /// Contains references to all services
        /// </summary>
        [DCInject]
        public IServicesHolder SS { get; set; }

        protected Account CurrentUser
        {
            get
            {
                if (SS == null || SS.AccountService == null)
                {
                    return null;
                }

                return SS.AccountService.CurrentUser;
            }
        }
    }
}
