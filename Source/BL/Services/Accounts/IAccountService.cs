﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using BL.Entities;
using BL.Common;

namespace BL.Services.Accounts
{
    public interface IAccountService : IEntityService<Account> 
    {
        Account CurrentUser { get; }

        OpResponce Create(CreateAccountRequest request);

        Account ValidateLogin(string username, string password);
    }
}
