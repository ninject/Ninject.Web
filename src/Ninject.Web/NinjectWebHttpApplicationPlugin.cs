// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectWebHttpApplicationPlugin.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2010, Enkari, Ltd.
//   Copyright (c) 2011-2020 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web
{
    using System.Web;

    using Ninject.Activation;
    using Ninject.Components;
    using Ninject.Web.Common;

    /// <summary>
    /// The web plugin implementation for ASP.NET application.
    /// </summary>
    public class NinjectWebHttpApplicationPlugin : NinjectComponent, INinjectHttpApplicationPlugin
    {
        /// <summary>
        /// The kernel.
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
        /// <param name="context">The context.</param>
        /// <returns>The request scope.</returns>
        public object GetRequestScope(IContext context)
        {
            return HttpContext.Current;
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