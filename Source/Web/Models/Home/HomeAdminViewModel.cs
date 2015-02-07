﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Web.Common;

using BL.Services.Accounts;

namespace Web.Models.Home
{
    public class HomeAdminViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 4)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role")]
        public Role UserRole { get; set; }

        public SelectList Roles
        {
            get
            {
                return MVCExtensions.ToSelectList(UserRole
                    , skipValues: new List<Role>() { Role.Unknown }
                    , addChooseItem: true
                    , chooseItemName: "");
            }
        }

        public HomeAccountsPartialViewModel AccountsModel { get; set; }
    }
}