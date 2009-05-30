#region License

//
// Author: Nate Kohari <nkohari@gmail.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

#region Using Directives

using System.Web;
using Ninject.Extensions.Logging;

#endregion

namespace Ninject.Web
{
    /// <summary>
    /// A <see cref="HttpApplication"/> that creates a <see cref="IKernel"/> for use throughout
    /// the application.
    /// </summary>
    public abstract class NinjectHttpApplication : HttpApplication
    {
        #region Properties

        /// <summary>
        /// Gets or sets the logger associated with the object.
        /// </summary>
        [Inject]
        public ILogger Logger { get; set; }

        #endregion

        #region Lifecycle Event Handlers

        /// <summary>
        /// Initializes the application.
        /// </summary>
        public void Application_Start()
        {
            // Create the Ninject kernel and store it in the static container.
            KernelContainer.Kernel = CreateKernel();

            // Request injections for the application itself.
            KernelContainer.Inject( this );

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