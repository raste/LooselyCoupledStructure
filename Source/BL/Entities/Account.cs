﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using BL.Services.Accounts;

namespace BL.Entities
{
    public class Account : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
