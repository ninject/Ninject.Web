Version 3.3.0
---------------
Updated project structure. Requires Ninject 3.3.4+

Version 3.2.1
---------------
Moved common bindings to Ninject.Web.Common so that multiple web extensions can be used together.

Version 3.0.0.0
---------------
- Added: App_Start/NinjectWeb.cs to NuGet package that registers the NinjectHttpModule
- Added: UserControlBase and WebControlBase
- Changed: Injection of DataBoundControls is delayed to the DataBound event to fix a problem with dynamic data
- Changed: This extension requires now Ninject.Web.Common. This allows combining it with the other web extesnions (WCF + MVC)
