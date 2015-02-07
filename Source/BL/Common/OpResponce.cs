﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BL.Common
{
    /// <summary>
    /// Base class for Service operations responces
    /// <para> Supports comparing to bool (responce == false)</para>
    /// </summary>
    public class OpResponce
    {
        public virtual bool Success { get; internal set; }
        public virtual string Message { get; internal set; }

        /// <summary>
        /// Success constructor
        /// </summary>
        public OpResponce()
            : this(true, string.Empty)
        {
        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="message"></param>
        public OpResponce(string message)
            : this(false, message)
        {
        }

        public OpResponce(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static bool operator ==(OpResponce responce, bool value)
        {
            if (responce == null)
            {
                return false;
            }

            return (responce.Success == value);
        }

        public static bool operator !=(OpResponce responce, bool value)
        {
            if (responce == null)
            {
                return false;
            }

            return (responce.Success != value);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
