using System;
using System.Linq;
using System.Reflection;

namespace Radix.Utils.Wpf
{
    /// <summary>
    /// Provides static product information from the AssemblyInfo of the entry assembly (the *.exe)
    /// </summary>
    public static class ProductInfo
    {
        /// <summary>
        /// Gets the first custom attribute of the specified type of the given assembly.
        /// </summary>
        /// <typeparam name="T">The type of attribute to return.</typeparam>
        /// <param name="assembly">The assembly to search for the attribute.</param>
        /// <returns>The first attribute of the given type that was found in the assembly, or null.</returns>
        public static T GetCustomAttribute<T>(Assembly assembly) where T : Attribute
        {
            return assembly?.GetCustomAttributes<T>()?.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first custom attribute of the specified type of the entry assembly (the *.exe).
        /// </summary>
        /// <typeparam name="T">The type of attribute to return.</typeparam>
        /// <returns>The first attribute of the given type that was found in the entry assembly, or null.</returns>
        public static T GetCustomAttribute<T>() where T : Attribute
        {
            return GetCustomAttribute<T>(Assembly.GetEntryAssembly());
        }

        //[assembly: AssemblyTitle("radix utility class library")]
        /// <summary>
        /// Gets the assembly title
        /// </summary>
        public static string AssemblyTitle => GetCustomAttribute<AssemblyTitleAttribute>()?.Title;

        //[assembly: AssemblyDescription("WPF component of the radix utility class library")]
        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        public static string AssemblyDescription => GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

        //[assembly: AssemblyConfiguration("")]
        /// <summary>
        /// Gets the assembly configuration.
        /// </summary>
        public static string AssemblyConfiguration => GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;

        //[assembly: AssemblyCompany("radix IT-Consulting & Engineering")]
        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        public static string AssemblyCompany => GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;

        //[assembly: AssemblyProduct("radix utility class library for C#")]
        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        public static string AssemblyProduct => GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        //[assembly: AssemblyCopyright("Copyright ©  2018 radix IT-Consulting & Engineering")]
        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        public static string AssemblyCopyright => GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

        //[assembly: AssemblyTrademark("")]
        /// <summary>
        /// Gets the assembly trademark.
        /// </summary>
        public static string AssemblyTrademark => GetCustomAttribute<AssemblyTrademarkAttribute>()?.Trademark;

        //[assembly: AssemblyCulture("")]
        /// <summary>
        /// Gets the assembly culture.
        /// </summary>
        public static string AssemblyCulture => GetCustomAttribute<AssemblyCultureAttribute>()?.Culture;

        //[assembly: AssemblyVersion("1.0.0.0")]
        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        public static string AssemblyVersion => GetCustomAttribute<AssemblyVersionAttribute>()?.Version;

        //[assembly: AssemblyFileVersion("1.0.0.0")]
        /// <summary>
        /// Gets the assembly file version.
        /// </summary>
        public static string AssemblyFileVersion => GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
    }
}
