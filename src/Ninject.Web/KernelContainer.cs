// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Web
{
    using System;
    using Ninject.Web.Common;

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
                    String.Format(
                        "The type {0} requested an injection, but no kernel has been registered for the web application.\r\n" +
                        "Please ensure that your project defines a NinjectHttpApplication.",
                        instance.GetType()));
            }

            kernel.Inject(instance);
        }
    }
}