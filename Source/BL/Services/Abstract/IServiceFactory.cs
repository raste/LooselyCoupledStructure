﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BL.Services
{
    /// <summary>
    /// Returns instances of services
    /// </summary>
    public interface IServiceFactory
    {
        T GetService<T>() where T : class;
    }
}
