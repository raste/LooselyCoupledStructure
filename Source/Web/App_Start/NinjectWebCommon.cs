﻿﻿// Loosely coupled MVC solution structure (https://github.com/raste/LooselyCoupledStructure)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Reflection;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Components;
    using Ninject.Selection.Heuristics;
    using Ninject.Web.Mvc.FilterBindingSyntax;

    using Web.Controllers;
    using Web.Common.Security;
    using Web.Common.Services;

    using BL.Repositories;
    using BL.Common.Attributes;
    using BL.Services;
    using BL.Services.Security;
    using BL.Services.Accounts;
    using BL.Services.Log;
    using BL.Services.Common;
    using BL.Services.Security.Cryptography;

    using DB;
    using DB.Repositories;

    /// <summary>
    /// Configuration of Ninject DC container
    /// </summary>
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            //Needed to overcome exceptions connected with controllers constructor injection
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));

            //Injects properties specified with custom attribute
            kernel.Components.Add<IInjectionHeuristic, CustomPropertyInjectionHeuristic>();

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //One connection per request -> creating Unit Of Work this way
            //When couple of repositories are making changes with Add/Update/Delete methods,
            //single call of Repository.Save() will create single transaction to DB for all changes 
            kernel.Bind<DBContext>().ToSelf().InRequestScope();

            kernel.Bind<IServiceFactory>().To<WebServiceFactory>().InRequestScope();

            kernel.Bind<IServicesHolder>().To<ServicesHolder>().InRequestScope();

            kernel.Bind<IAccountRepository>().To<AccountRepository>().InRequestScope();

            kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            kernel.Bind<ILogService>().To<ElmahLogService>().InRequestScope();
            kernel.Bind<ICryptoService>().To<CryptoService>().InRequestScope();

            kernel.Bind<IAuthenticateService>().To<WebAuthenticationService>().InRequestScope();
            kernel.Bind<IConfigurationService>().To<WebConfigurationService>().InRequestScope();

            kernel.BindFilter<WebAuthorizeFilter>(FilterScope.Controller, 0)
                .WhenControllerHas<WebAuthorizeAttribute>()
                .WithConstructorArgumentFromControllerAttribute<WebAuthorizeAttribute>("roles", attr => attr.Roles);

            kernel.BindFilter<WebAuthorizeFilter>(FilterScope.Action, 0)
               .WhenActionMethodHas<WebAuthorizeAttribute>()
               .WithConstructorArgumentFromActionAttribute<WebAuthorizeAttribute>("roles", attr => attr.Roles);
        }        
    }

    /// <summary>
    /// Needed to overcome controllers constructor injection errors. 
    /// <para> Controller constructor injection is not working with default Ninect Package Instalation: Ninject + Ninject.MVC. </para>
    /// <para> It can be fixed with this factory ot with additional settings in web.config. </para>
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        
        public NinjectControllerFactory(IKernel kernel)
        {
            ninjectKernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (controllerType == null) ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
    
    public class CustomPropertyInjectionHeuristic : NinjectComponent, IInjectionHeuristic
    {
        private readonly IKernel kernel;

        public CustomPropertyInjectionHeuristic(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool ShouldInject(MemberInfo memberInfo)
        {
            var propertyInfo = (memberInfo as PropertyInfo);

            if (propertyInfo == null)
            {
                return false;
            }

            if (propertyInfo.CanWrite == false)
            {
                return false;
            }

            if (ShouldInjectIfHasCustomInjectAttribute(memberInfo, propertyInfo) == true)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Injects properties to injected objects marked with [DCInject] attribute
        /// </summary>
        private bool ShouldInjectIfHasCustomInjectAttribute(MemberInfo memberInfo, PropertyInfo propertyInfo)
        {
            Type declaringType = memberInfo.DeclaringType;
            Type propertyType = propertyInfo.PropertyType;

            if (propertyType == declaringType
                || propertyType == memberInfo.ReflectedType 
                || propertyType.IsAssignableFrom(declaringType) == true)
            {   //trying to preventing circular references
                //NOTE: it will not work if the property is declared in base class, and the property type is the parent class
                return false;
            }

            var inject =  memberInfo.IsDefined(typeof(DCInjectAttribute), true);
            return inject;
        }

 
    }

 
}
