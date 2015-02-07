﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Collections.Generic;
using System.Linq;

using BL.Entities;
using BL.Repositories;
using BL.Common;

namespace BL.Services.Accounts
{
    public class AccountService : EntityServiceBase<Account, IAccountRepository>, IAccountService
    {
        private Account currentUser = null;
        public new Account CurrentUser
        {
            get 
            {
                if (currentUser != null)
                {
                    return currentUser;
                }

                if (SS.AuthenticationService.CurrentUserId.HasValue == false)
                {
                    return null;
                }

                currentUser = GetSingleNonDeleted(SS.AuthenticationService.CurrentUserId.Value);
                return currentUser;
            }
        }

        public AccountService(IAccountRepository repository)
            : base(repository)
        {
            
        }

        public override IQueryable<Account> GetAllInContext()
        {
            if (CurrentUser == null)
            {
                return new List<Account>().AsQueryable();
            }

            if (CurrentUser.Role == Role.Administrator)
            {
                return base.GetAllInContext();
            }

            return base.GetAllInContext().Where(a => a.Role != Role.Administrator);
        }

        public OpResponce Create(CreateAccountRequest request)
        {
            var executor = new CreateAccountExecutor(this, request);
            return executor.Create();
        }

        public Account ValidateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) == true || string.IsNullOrEmpty(password) == true)
            {
                return null;
            }

            username = username.Trim();
            var account = this.GetAllNonDeleted().FirstOrDefault(u => u.Username == username);
            if (account == null)
            {
                return null;
            }

            password = password.Trim();
            if (SS.CryptoService.Compare(account.Password, password) == false)
            {
                return null;
            }

            return account;
        }
    }
}
