﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BL.Services.Security
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// The current user ID taken from the authorization information (Web site: cookie, API: http request header)
        /// </summary>
        int? CurrentUserId { get; }

        /// <summary>
        /// When Authenticating from log in form
        /// </summary>
        void Authenticate(int userId, bool persist);

        /// <summary>
        /// When Signing Out
        /// </summary>
        void DeAuthenticate();
    }
}
