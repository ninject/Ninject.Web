#region License

//
// Author: David Hayden <davehayden@gmail.com>
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