﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using BL.Services.Accounts;
using BL.Services.Security;
using BL.Services.Log;
using BL.Services.Common;
using BL.Services.Security.Cryptography;

namespace BL.Services
{
    /// <summary>
    /// Contains referencies to all services.
    /// <para> The services are loaded only when they are requested (lazy loading)</para>
    /// </summary>
    public class ServicesHolder : IServicesHolder
    {
        private IServiceFactory factory;

        public ServicesHolder(IServiceFactory factory)
        {
            this.factory = factory;
        }

        public IAccountService AccountService { get { return factory.GetService<IAccountService>(); } }

        public IAuthenticateService AuthenticationService { get { return factory.GetService<IAuthenticateService>(); } }

        public ILogService LogService { get { return factory.GetService<ILogService>(); } }

        public IConfigurationService ConfigurationService { get { return factory.GetService<IConfigurationService>(); } }

        public ICryptoService CryptoService { get { return factory.GetService<ICryptoService>(); } }
    }
}
