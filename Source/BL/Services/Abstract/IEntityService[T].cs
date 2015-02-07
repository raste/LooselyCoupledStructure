﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Linq;

using BL.Entities;

namespace BL.Services
{
    /// <summary>
    /// Exposes common GET methods for services with scope around entities.
    /// <para> Inherit from services wrapped around entities</para>
    /// </summary>
    public interface IEntityService<T> where T : class, IEntity
    {
        /// <summary>
        /// All entities filtered to apply to current scope.
        /// </summary>
        IQueryable<T> GetAllInContext();

        /// <summary>
        /// All entities which are not deleted.
        /// </summary>
        IQueryable<T> GetAllNonDeleted();

        /// <summary>
        /// All entities.
        /// </summary>
        IQueryable<T> GetEverything();

        T GetSingleInContext(int id);

        T GetSingleNonDeleted(int id);
    }
}
