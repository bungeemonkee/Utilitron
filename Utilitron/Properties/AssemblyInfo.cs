using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Utilitron")]
[assembly: AssemblyDescription("Basic .NET utilities.")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("David love")]
[assembly: AssemblyProduct("Utilitron")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("089b3411-e1ca-4812-8daa-92d332f72408")]

// Version information for an assembly consists of the following four values.
// We will increase these values in the following way:
//    Major Version : Increased when there is a release that breaks a public api
//    Minor Version : Increased for each non-api-breaking release
//    Build Number : 0 for alpha versions, 1 for beta versions, 2 for release candidates, 3 for releases
//    Revision : Always 0 for release versions, always 1+ for alpha, beta, rc versions to indicate the alpha/beta/rc number
[assembly: AssemblyVersion("1.0.0.11")]
[assembly: AssemblyFileVersion("1.0.0.11")]

// This version number will roughly follow semantic versioning : http://semver.org
// The first three numbers will always match the first the numbers of the version above.
[assembly: AssemblyInformationalVersion("1.0.0-alpha11")]
