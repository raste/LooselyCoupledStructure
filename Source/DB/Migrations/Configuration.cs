﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    using BL.Entities;
    using BL.Services.Accounts;

    internal sealed class Configuration : DbMigrationsConfiguration<DB.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DB.DBContext context)
        {
            //This method is called after migrating to the latest version. 
            //In other words: on first DBContext query after project start.

            context.Set<Account>().AddOrUpdate(a => a.Username,
                new Account()
                {
                    Username = "Admin",
                    Password = string.Format("{0}{1}{2}"
                        , "1000:QDw9RpNSJJtC6PToZjcsUpz68KQQwqd+Jt4Zpw==:+QIWlvuGPsdSdk3QbNuVs+Ip15DauEQIS5qRABDb3yfjA+DsKmwBpHNcquDhiv"
                        , "Ig9G3O574DoW8cs7tf2G6Ud3s2aOr7C09OGIZjLX2dSpA7Mbg31X+3JvNl3tyDCOO6J+OtC2amaYG/8QTGkWg3Jn5uBKSAcYEhPrGRJme4ur9T"
                        , "x4d7sKfJDS7ZA1SZafUwWzR88kXLBmo7QQ3/0T4xcDjIgrfCJQyaGU3fvTOziK45O5cNBywvwVRvRzh+arXSc/bNJGq8aSk="),
                    Deleted = false,
                    Role = Role.Administrator,
                    CreatedDate = DateTime.UtcNow
                }
            );
        }
    }
}
