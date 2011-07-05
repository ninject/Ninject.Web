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