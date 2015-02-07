﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;

namespace BL.Services.Log
{
    public interface ILogService
    {
        void Log(string message);
        void Log(Exception ex);
        void Log(string context, Exception ex);
    }
}
