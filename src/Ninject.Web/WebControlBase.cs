#region License
//
// Author: Nate Kohari <nkohari@gmail.com>, Nik Radford <nikradford@googlemail.com>
// Copyright (c) 2007-2008, Enkari, Ltd.
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

using System;
using System.Web.UI.WebControls;

namespace Ninject.Web
{
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
            RequestActivation();
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