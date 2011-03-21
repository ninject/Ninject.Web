//-------------------------------------------------------------------------------
// <copyright file="NinjectWebHttpApplicationPlugin.cs" company="bbv Software Services AG">
//   Copyright (c) 2011 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Web
{
    using System.Web;
    using Ninject.Components;
    using Ninject.Web.Common;

    /// <summary>
    /// The web plugin implementation for MVC
    /// </summary>
    public class NinjectWebHttpApplicationPlugin : NinjectComponent, INinjectHttpApplicationPlugin
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectWebHttpApplicationPlugin"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectWebHttpApplicationPlugin(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Gets the request scope.
        /// </summary>
        /// <value>The request scope.</value>
        public object RequestScope
        {
            get
            {
                return HttpContext.Current;
            }
        }
        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            KernelContainer.Kernel = this.kernel;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
        }
    }
}