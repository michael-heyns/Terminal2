using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// NOTES
// https://docs.microsoft.com/en-us/dotnet/desktop/winforms/high-dpi-support-in-windows-forms?view=netframeworkdesktop-4.8


// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Terminal2")]
[assembly: AssemblyDescription("Serial/TCP Terminal Emulator")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Terminal2")]
[assembly: AssemblyCopyright("Copyright ©2022 Michael Heyns")]
[assembly: AssemblyTrademark("Please adhere to the license requirements")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("63096c73-5e1f-4331-94f9-3c09c07e02db")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.3.0")]
[assembly: AssemblyFileVersion("1.2.3.0")]

namespace Terminal
{
    public class AssemblyInfo
    {
        // The assembly information values.
        public string Title = "", Description = "", Company = "",
            Product = "", Copyright = "", Trademark = "",
            AssemblyVersion = "", FileVersion = "", Guid = "",
            NeutralLanguage = "";
        public bool IsComVisible = false;

        // Constructors.
        public AssemblyInfo()
            : this(Assembly.GetExecutingAssembly())
        {
        }

        public AssemblyInfo(Assembly assembly)
        {
            // Get values from the assembly.
            AssemblyTitleAttribute titleAttr =
                GetAssemblyAttribute<AssemblyTitleAttribute>(assembly);
            if (titleAttr != null)
                Title = titleAttr.Title;

            AssemblyDescriptionAttribute assemblyAttr =
                GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly);
            if (assemblyAttr != null)
                Description = assemblyAttr.Description;

            AssemblyCompanyAttribute companyAttr =
                GetAssemblyAttribute<AssemblyCompanyAttribute>(assembly);
            if (companyAttr != null)
                Company = companyAttr.Company;

            AssemblyProductAttribute productAttr =
                GetAssemblyAttribute<AssemblyProductAttribute>(assembly);
            if (productAttr != null)
                Product = productAttr.Product;

            AssemblyCopyrightAttribute copyrightAttr =
                GetAssemblyAttribute<AssemblyCopyrightAttribute>(assembly);
            if (copyrightAttr != null)
                Copyright = copyrightAttr.Copyright;

            AssemblyTrademarkAttribute trademarkAttr =
                GetAssemblyAttribute<AssemblyTrademarkAttribute>(assembly);
            if (trademarkAttr != null)
                Trademark = trademarkAttr.Trademark;

            AssemblyVersion = assembly.GetName().Version.ToString();

            AssemblyFileVersionAttribute fileVersionAttr =
                GetAssemblyAttribute<AssemblyFileVersionAttribute>(assembly);
            if (fileVersionAttr != null)
                FileVersion = fileVersionAttr.Version;

            GuidAttribute guidAttr = GetAssemblyAttribute<GuidAttribute>(assembly);
            if (guidAttr != null)
                Guid = guidAttr.Value;

            NeutralResourcesLanguageAttribute languageAttr =
                GetAssemblyAttribute<NeutralResourcesLanguageAttribute>(assembly);
            if (languageAttr != null)
                NeutralLanguage = languageAttr.CultureName;

            ComVisibleAttribute comAttr =
                GetAssemblyAttribute<ComVisibleAttribute>(assembly);
            if (comAttr != null)
                IsComVisible = comAttr.Value;
        }

        // Return a particular assembly attribute value.
        public static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
        {
            // Get attributes of this type.
            object[] attributes = assembly.GetCustomAttributes(typeof(T), true);

            // If we didn't get anything, return null.
            if ((attributes == null) || (attributes.Length == 0))
                return null;

            // Convert the first attribute value into the desired type and return it.
            return (T)attributes[0];
        }
    }
}
