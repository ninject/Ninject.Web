// -------------------------------------------------------------------------------------------------
// <copyright file="KernelContainer.cs" company="Ninject Project Contributors">
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
    using System;

    using Ninject.Web.Common.WebHost;

    /// <summary>
    /// A static container for the <see cref="NinjectHttpApplication"/>'s kernel.
    /// </summary>
    internal static class KernelContainer
    {
        /// <summary>
        /// The ninject kernel.
        /// </summary>
        private static IKernel kernel;

        /// <summary>
        /// Gets or sets the kernel that is used in the application.
        /// </summary>
        public static IKernel Kernel
        {
            get
            {
                return kernel;
            }

            set
            {
                if (kernel != null)
                {
                    throw new NotSupportedException("The static container already has a kernel associated with it!");
                }

                kernel = value;
            }
        }

        /// <summary>
        /// Injects the specified instance by using the container's kernel.
        /// </summary>
        /// <param name="instance">The instance to inject.</param>
        public static void Inject(object instance)
        {
            if (kernel == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The type {0} requested an injection, but no kernel has been registered for the web application.\r\n" +
                        "Please ensure that your project defines a NinjectHttpApplication.",
                        instance.GetType()));
            }

            kernel.Inject(instance);
        }
    }
}