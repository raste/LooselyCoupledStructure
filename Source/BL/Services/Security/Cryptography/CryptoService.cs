﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using BL.Services.Common;

namespace BL.Services.Security.Cryptography
{
    public class CryptoService : ICryptoService
    {
        private IConfigurationService configService = null;

        public CryptoService(IConfigurationService configService)
        {
            this.configService = configService;
        }

        public string Hash(string plainText)
        {
            var executor = new HashExecutor(configService);
            return executor.Hash(plainText);
        }

        public bool Compare(string hashedText, string plainText)
        {
            var executor = new HashExecutor(configService);
            return executor.Compare(hashedText, plainText);
        }
    }
}
