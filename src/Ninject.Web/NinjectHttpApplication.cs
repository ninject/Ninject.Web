#region License

// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace Ninject.Web
{
    using System.Web;

    /// <summary>
    /// A <see cref="HttpApplication"/> that creates a <see cref="IKernel"/> for use throughout
    /// the application.
    /// </summary>
    public abstract class NinjectHttpApplication : HttpApplication
    {
        #region Lifecycle Event Handlers
        /// <summary>
        /// The one per request module to release request scope at the end of the request
        /// </summary>
        private readonly OnePerRequestModule onePerRequestModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectHttpApplication"/> class.
        /// </summary>
        protected NinjectHttpApplication()
        {
            this.onePerRequestModule = new OnePerRequestModule();
            this.onePerRequestModule.Init(this);
        }
        
        /// <summary>
        /// Initializes the application.
        /// </summary>
        public void Application_Start()
        {
            // Create the Ninject kernel and store it in the static container.
            KernelContainer.Kernel = CreateKernel();

            // Request injections for the application itself.
            KernelContainer.Inject( this );

            if (KernelContainer.Kernel.Settings.Get("ReleaseScopeAtRequestEnd", true))
            {
                OnePerRequestModule.StartManaging(KernelContainer.Kernel);
            }

            OnApplicationStarted();
        }

        /// <summary>
        /// Finalizes the application.
        /// </summary>
        public void Application_End()
        {
            OnApplicationEnded();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected virtual void OnApplicationStarted()
        {
        }

        /// <summary>
        /// Called when the application ends.
        /// </summary>
        protected virtual void OnApplicationEnded()
        {
        }

        /// <summary>
        /// Creates a Ninject kernel that will be used to inject objects.
        /// </summary>
        /// <returns>The created kernel.</returns>
        protected abstract IKernel CreateKernel();

        #endregion
    }
}