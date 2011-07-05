using System;
using System.Web.UI; 

namespace Ninject.Web
{
    public class UserControlBase : UserControl
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
