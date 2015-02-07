﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Collections.Generic;
using System.Linq;

using BL.Entities;
using BL.Services.Accounts;

namespace Web.Models.Home
{
    public class HomeAccountsPartialViewModel
    {
        public List<User> Users { get; private set; }

        public HomeAccountsPartialViewModel(IQueryable<Account> accounts)
        {
            Users = new List<User>();

            var dbUsers = accounts
                .Select(a => new { a.Username, a.Role })
                .ToList();

            if (dbUsers == null || dbUsers.Count == 0)
            {
                return;
            }

            Users = dbUsers
                .Select(a => new User() { Username = a.Username, Role = a.Role })
                .ToList();
        }

        public class User
        {
            public string Username { get; set; }
            public Role Role { get; set; }
        }
    }
}