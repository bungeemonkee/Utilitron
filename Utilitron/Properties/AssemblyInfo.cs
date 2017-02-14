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

[assembly: AssemblyCompany("David Love")]
[assembly: AssemblyProduct("Utilitron")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en-US")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("73c0ccc7-fc6f-4c3e-b6e7-41cc05a57f7c")]

[assembly: AssemblyVersion("1.0.0.17")]
[assembly: AssemblyFileVersion("1.0.0.17")]

// This version number will roughly follow semantic versioning : http://semver.org
// The first three numbers will always match the first the numbers of the version above.
// This must alos be updated in project.json
[assembly: AssemblyInformationalVersion("1.0.0-alpha17")]
