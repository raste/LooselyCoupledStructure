﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using BL.Repositories;

namespace DB.Repositories
{
    /// <summary>
    /// Implements the common repository methods
    /// <para> Inherit from each repository in this project </para>
    /// </summary>
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private DBContext context;
        private DbSet<T> dbSet;

        public Repository(DBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Add(List<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Delete(List<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
