﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BL.Services.Security.Cryptography
{
    /// <summary>
    /// Provides crypto operations
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Returns a hashed input
        /// </summary>
        string Hash(string plainText);

        /// <summary>
        /// Compares hashed text with input  
        /// </summary>
        bool Compare(string hashedText, string plainText);  
    }
}
