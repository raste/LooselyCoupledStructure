﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Web;

using Elmah;

namespace BL.Services.Log
{
    public class ElmahLogService : ILogService
    {
        public void Log(string message)
        {
            var exception = new Exception(message);
            Raise(exception);
        }

        public void Log(Exception ex)
        {
            Raise(ex);
        }

        public void Log(string context, Exception ex)
        {
            var exception = new Exception(context, ex);
            Raise(exception);
        }

        private void Raise(Exception ex)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    LogExceptionWithContext(ex);
                }
                else
                {   
                    LogExceptionWithoutContext(ex);
                }
            }
            catch 
            { 
                //Nothing can be done..
            }
        }

        private void LogExceptionWithContext(Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }

        private void LogExceptionWithoutContext(Exception ex)
        {
            var mail = new ElmahMail();
            mail.Log(ex);

            var logger = new ElmahLog();
            logger.Log(ex);

            //Add additional modules if other types of logging must be supported (Ex. twitter messages)
            //when there is no http context (for console/service/win forms applications)
        }
    }


    internal class ElmahMail : ErrorMailModule
    {
        public ElmahMail()
        {
            base.OnInit(new HttpApplication());
        }

        public void Log(Exception ex)
        {
            var error = new Error(ex);
            base.ReportError(error);
        }
    }

    internal class ElmahLog : ErrorLogModule
    {
        public ElmahLog()
        {
            base.OnInit(new HttpApplication());
        }

        public void Log(Exception ex)
        {
            base.LogException(ex, null);
        }
    }
}
