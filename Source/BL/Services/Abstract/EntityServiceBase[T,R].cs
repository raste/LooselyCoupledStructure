﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;

using BL.Entities;
using BL.Repositories;

namespace BL.Services
{
    /// <summary>
    /// Base class for Services around Entities
    /// <para> Extends repository Create, Update methods with filling the history fields </para>
    /// <para> Catches and logs Repository exceptions (for save operations) </para>
    /// <para> Provides access to other services by inheriting ServicesContainer </para>
    /// <para> Inherit if the service is wrapper around entity </para>
    /// </summary>
    /// <typeparam name="T"> The type of Entity </typeparam>
    /// <typeparam name="R"> The type of the Repository which performs CRUD operations with the Entity </typeparam>
    public abstract class EntityServiceBase<T, R> : ServicesContainer, IEntityService<T>
        where T : class, IEntity
        where R : IRepository<T>
    {
        protected R repository;

        public EntityServiceBase(R repository)
            : base()
        {
            this.repository = repository;
        }

        //Override in the inherited class for concrete filtering based on the current context
        public virtual IQueryable<T> GetAllInContext()
        {
            return GetAllNonDeleted();
        }

        public IQueryable<T> GetAllNonDeleted()
        {
            return GetEverything().Where(e => e.Deleted == false);
        }

        public IQueryable<T> GetEverything()
        {
            return repository.GetAll();
        }

        public virtual T GetSingleInContext(int id)
        {
            return GetAllInContext().FirstOrDefault(e => e.ID == id);
        }

        public T GetSingleNonDeleted(int id)
        {
            return GetAllNonDeleted().FirstOrDefault(e => e.ID == id);
        }

        public void Add(T entity)
        {
            SetCreateFields(entity);

            repository.Add(entity);
        }

        public void Add(List<T> entities)
        {
            SetCreateFields(entities);

            repository.Add(entities);
        }

        private void SetCreateFields(T entity)
        {
            entity.CreatedBy = (CurrentUser != null
                ? CurrentUser.ID
                : default(int?));

            entity.CreatedDate = DateTime.UtcNow;
        }

        private void SetCreateFields(List<T> entities)
        {
            foreach (var entity in entities)
            {
                SetCreateFields(entity);
            }
        }

        public void Update(T entity)
        {
            SetlastModifiedFields(entity);

            repository.Update(entity);
        }

        public void Update(List<T> entities)
        {
            SetlastModifiedFields(entities);

            repository.Update(entities);
        }

        private void SetlastModifiedFields(T entity)
        {
            entity.LastModifiedBy = (CurrentUser != null
                ? CurrentUser.ID
                : default(int?));

            entity.LastModifiedDate = DateTime.UtcNow;
        }

        private void SetlastModifiedFields(List<T> entities)
        {
            foreach (var entity in entities)
            {
                SetlastModifiedFields(entity);
            }
        }

        public void Delete(T entity)
        {
            repository.Delete(entity);
        }

        public void Delete(List<T> entities)
        {
            repository.Delete(entities);
        }

        public bool Save()
        {
            try
            {
                repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                SS.LogService.Log(ex);

                return false;
            }
        }

    }
}
