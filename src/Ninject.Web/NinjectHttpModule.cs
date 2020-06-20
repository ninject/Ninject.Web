// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectHttpModule.cs" company="Ninject Project Contributors">
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
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject.Infrastructure.Disposal;

    /// <summary>
    /// An <see cref="IHttpModule"/> that injects dependencies into pages and usercontrols.
    /// </summary>
    public class NinjectHttpModule : DisposableObject, IHttpModule
    {
        /// <summary>
        /// The http application managed by this module.
        /// </summary>
        private HttpApplication httpApplication;

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">A <see cref="HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application.</param>
        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.httpApplication = context;
            this.httpApplication.PreRequestHandlerExecute += this.OnPreRequestHandlerExecute;
        }

        /// <summary>
        /// Search for usercontrols within the parent control
        /// and inject their dependencies using KernelContainer.
        /// </summary>
        /// <param name="parent">The parent control.</param>
        /// <param name="skipDataBoundControls">if set to <c>true</c> special handling of DataBoundControls is skipped.</param>
        private static void InjectUserControls(Control parent, bool skipDataBoundControls)
        {
            if (parent == null)
            {
                return;
            }

            if (skipDataBoundControls)
            {
                var dataBoundControl = parent as DataBoundControl;
                if (dataBoundControl != null)
                {
                    dataBoundControl.DataBound += InjectDataBoundControl;
                    return;
                }
            }

            foreach (Control control in parent.Controls)
            {
                if (control is UserControl)
                {
                    KernelContainer.Inject(control);
                }

                InjectUserControls(control, skipDataBoundControls);
            }
        }

        /// <summary>
        /// Injects a data bound control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void InjectDataBoundControl(object sender, EventArgs e)
        {
            var dataBoundControl = sender as DataBoundControl;
            if (dataBoundControl != null)
            {
                dataBoundControl.DataBound -= InjectDataBoundControl;
                InjectUserControls(dataBoundControl, false);
            }
        }

        /// <summary>
        /// Injects dependencies into web pages and subscribes to their InitComplete
        /// Event to inject usercontrols with their dependencies.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            var page = this.httpApplication.Context.CurrentHandler as Page;

            if (page == null)
            {
                return;
            }

            KernelContainer.Inject(page);
            page.PreLoad += (src, args) => InjectUserControls(page, false);
        }
    }
}