#region License

// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.Web;
using System.Web.UI;
using Ninject.Infrastructure.Disposal;

#endregion

namespace Ninject.Web
{
    /// <summary>
    /// An <see cref="IHttpModule"/> that injects dependencies into pages and usercontrols.
    /// </summary>
    public class NinjectHttpModule : DisposableObject, IHttpModule
    {
        private HttpApplication _application;

        #region IHttpModule Members

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">A <see cref="HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init( HttpApplication context )
        {
            if ( context == null )
            {
                throw new ArgumentNullException( "context" );
            }

            _application = context;
            _application.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        #endregion

        /// <summary>
        /// Injects dependencies into web pages and subscribes to their InitComplete
        /// Event to inject usercontrols with their dependencies.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnPreRequestHandlerExecute( object sender, EventArgs e )
        {
            var page = _application.Context.CurrentHandler as Page;

            if ( page == null )
            {
                return;
            }

            KernelContainer.Inject( page );
            page.InitComplete += ( src, args ) => InjectUserControls( page );
        }

        /// <summary>
        /// Search for usercontrols within the parent control
        /// and inject their dependencies using KernelContainer.
        /// </summary>
        /// <param name="parent">The parent control.</param>
        private static void InjectUserControls( Control parent )
        {
            if ( parent == null || parent.Controls == null )
            {
                return;
            }

            foreach ( Control control in parent.Controls )
            {
                if ( control is UserControl )
                {
                    KernelContainer.Inject( control );
                }

                InjectUserControls( control );
            }
        }
    }
}