﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Collections.Generic;
using System.Linq;

namespace BL.Repositories
{
    /// <summary>
    /// Exposes common repository functionality
    /// <para> Add/Update/Delete do not update the database until Save() is called </para>
    /// <para> Inherit from Repository interface </para>
    /// </summary>
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        void Add(T entity);
        void Add(List<T> entities);

        void Update(T entity);
        void Update(List<T> entities);

        void Delete(T entity);
        void Delete(List<T> entities);

        /// <summary>
        /// Saves the changes made from Add/Updated/Delete methods
        /// </summary>
        void Save();
    }
}
