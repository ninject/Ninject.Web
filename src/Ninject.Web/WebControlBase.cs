// -------------------------------------------------------------------------------------------------
// <copyright file="WebControlBase.cs" company="Ninject Project Contributors">
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
    using System.Web.UI.WebControls;

    /// <summary>
    /// A <see cref="WebControl"/> that supports injections.
    /// </summary>
    public class WebControlBase : WebControl
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.WebControl.Init"></see> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.RequestActivation();
        }

        /// <summary>
        /// Asks the kernel to inject this instance.
        /// </summary>
        protected virtual void RequestActivation()
        {
            KernelContainer.Inject(this);
        }
    }
}