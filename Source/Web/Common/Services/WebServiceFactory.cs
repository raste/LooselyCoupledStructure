﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System.Web.Mvc;

using BL.Services;

namespace Web.Common.Services
{
    public class WebServiceFactory : IServiceFactory
    {
        public T GetService<T>() where T : class
        {
            var service = DependencyResolver.Current.GetService(typeof(T));
            return (T)service;
        }
    }
}