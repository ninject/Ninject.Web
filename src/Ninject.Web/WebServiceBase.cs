// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Web
{
    using System.Web.Services;

    /// <summary>
    /// A <see cref="WebService"/> that supports injections.
    /// </summary>
    public abstract class WebServiceBase : WebService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceBase"/> class.
        /// </summary>
        protected WebServiceBase()
        {
            KernelContainer.Inject(this);
        }
    }
}