﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Linq;

using BL.Entities;
using BL.Common;

namespace BL.Services.Accounts
{
    internal class CreateAccountExecutor
    {
        private AccountService accountService;

        private CreateAccountRequest request;

        public CreateAccountExecutor(AccountService accountService
            , CreateAccountRequest request)
        {
            this.accountService = accountService;

            this.request = request;
            this.request.Password = this.request.Password.Trim();
            this.request.Username = this.request.Username.Trim();
        }

        public OpResponce Create()
        {
            var responce = ValidateInputs();
            if (responce == false)
            {
                return responce;
            }

            responce = ValidateCurrentUser();
            if (responce == false)
            {
                return responce;
            }

            responce = ValidateUsernameNotTaken();
            if (responce == false)
            {
                return responce;
            }

            responce = CreateUser();
            return responce;
        }

        private OpResponce ValidateInputs()
        {
            if (string.IsNullOrEmpty(request.Username) == true)
            {
                return new OpResponce("Enter username");
            }

            if (string.IsNullOrEmpty(request.Password) == true)
            {
                return new OpResponce("Enter password");
            }

            if (request.Role == Role.Unknown)
            {
                throw new ArgumentException(string.Format("Role {0} is not supported.", request.Role.ToString()));
            }

            return new OpResponce();
        }

        private OpResponce ValidateCurrentUser()
        {
            if (accountService.CurrentUser == null)
            {
                return new OpResponce("Unsufficient rights!");
            }

            if (accountService.CurrentUser.Role != Role.Administrator
                && request.Role == Role.Administrator)
            {
                return new OpResponce("Unsufficient rights!");
            }

            return new OpResponce();
        }


        private OpResponce ValidateUsernameNotTaken()
        {
            var createdAccount = accountService.GetAllNonDeleted()
                .FirstOrDefault(u => u.Username == request.Username);

            if (createdAccount != null)
            {
                return new OpResponce("Username in use");
            }

            return new OpResponce();
        }

        private OpResponce CreateUser()
        {
            string hashedPass = accountService.SS.CryptoService.Hash(request.Password);

            var newAccount = new Account()
            {
                Username = request.Username,
                Password = hashedPass,
                Role = request.Role
            };

            accountService.Add(newAccount);

            if (accountService.Save() == false)
            {
                return new OpResponce("Oops, something happened, please try again");
            }

            return new OpResponce();
        }
    }
}
