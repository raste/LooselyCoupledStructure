﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Configuration;

using BL.Services.Common;
using BL.Common;

namespace Web.Common.Services
{
    public class WebConfigurationService : IConfigurationService
    {
        private const string HASH_SALT_SIZE_SETTING_NAME = "security:hash:salt:size";

        private const string HASH_SIZE_WITHOUT_SALT_SETTING_NAME = "security:hash:hash_text:size_w/o_salt";

        public int SecurityHashSaltSize()
        {
            return GetIntSetting(HASH_SALT_SIZE_SETTING_NAME);
        }

        public int SecurityHashLengthWOSalt()
        {
            return GetIntSetting(HASH_SIZE_WITHOUT_SALT_SETTING_NAME);
        }

        private int GetIntSetting(string setting, bool excIfEmpty = true)
        {
            string strValue = GetStringSetting(setting, excIfEmpty);

            if (strValue.IsEmpty() == true)
            {
                return 0;
            }

            return int.Parse(strValue);
        }

        private string GetStringSetting(string setting, bool excIfEmpty = true)
        {
            string strValue = ConfigurationManager.AppSettings[setting];

            if (strValue.IsEmpty() == true && excIfEmpty == true)
            {
                ThrowEmptySettingValueException(setting);
            }

            return strValue;
        }

        private void ThrowEmptySettingValueException(string setting)
        {
            throw new ArgumentException(string.Format("Setting {0} value is not set.", setting));
        }
    }
}