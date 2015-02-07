﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BL.Services.Common
{
    /// <summary>
    /// Retrieves values of customizable settings
    /// </summary>
    public interface IConfigurationService
    {
        //Put methods which return values of other customizable settings

        /// <summary>
        /// Wanted length of base 64 string variant of the salt bytes
        /// <para> Need to be modulus of 4 (24,28...60..200..216..)</para>
        /// </summary>
        int SecurityHashSaltSize();

        /// <summary>
        /// Bytes size of the hash, without the appended salt ot it.
        /// <para> Note: this is not the length of the base 64 string variant of the bytes (it will be bigger) </para>
        /// </summary>
        int SecurityHashLengthWOSalt();
    }
}
