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
    /// Contains referencies to all services
    /// </summary>
    public interface IServicesHolder
    {
        IAccountService AccountService { get; }

        IAuthenticateService AuthenticationService { get; }

        ILogService LogService { get; }

        IConfigurationService ConfigurationService { get; }

        ICryptoService CryptoService { get; }
    }
}
