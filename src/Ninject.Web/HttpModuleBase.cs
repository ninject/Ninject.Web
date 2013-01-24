namespace Ninject.Web
{
    using System;
    using System.Web;

    /// <summary>
    /// A <see cref="IHttpModule"/> that supports injections.
    /// </summary>
    public abstract class HttpModuleBase : IHttpModule
    {
        /// <summary>
        /// This method is unused by the base class.
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Ininitialize the module and request injection.
        /// </summary>
        /// <param name="context">The current <see cref="T:System.Web.HttpApplication"></see> instance</param>
        public virtual void Init(HttpApplication context)
        {
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