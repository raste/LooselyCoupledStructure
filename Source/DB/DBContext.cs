﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Data.Entity;

using DB.Migrations;

using BL.Entities;

namespace DB
{
    public class DBContext : DbContext
    {
        private const string CONNECTION_STRING_NAME = "DBContext";

        static DBContext()
        {   // This makes sure that Database will be created (if no such) and updated

            var initializer = new MigrateDatabaseToLatestVersion<DBContext, Configuration>();

            Database.SetInitializer(initializer);

            if (Database.Exists(CONNECTION_STRING_NAME) == false)
            {   //Forcing Database creation and seeding if it doesn't exists.
                //It will be created also without this block, but it won't be seeded immediately afterwards. 
                //(You will have to run the project again in order to seed)
                initializer.InitializeDatabase(new DBContext());
            }
        }

        public DBContext()
            : base(CONNECTION_STRING_NAME)
        {

        }

        //Add Entities representing tables here - they will be added automatically to the database

        public DbSet<Account> Accounts { get; set; }
    }
}
