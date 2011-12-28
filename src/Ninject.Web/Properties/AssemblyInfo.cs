using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle("Ninject ASP.NET Framework")]
[assembly: AssemblyDescriptionAttribute("ASP.NET application bootstrapper for Ninject")]
[assembly: Guid("2dafe407-a883-46fb-b13d-3263716ca817")]

#if !NO_PARTIAL_TRUST
[assembly: AllowPartiallyTrustedCallers]
#endif