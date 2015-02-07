﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BL.Services.Accounts
{
    /// <summary>
    /// Accounts Roles
    /// </summary>
    public enum Role : int
    {
        /// <summary>
        /// Default, should not be used
        /// </summary>
        Unknown = 0,
        Administrator = 1,
        Manager = 2
    }
}
