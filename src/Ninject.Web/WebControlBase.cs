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
    public class WebControlBase : WebControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RequestActivation();
        }

        protected virtual void RequestActivation()
        {
            KernelContainer.Inject(this);
        }
    }
}