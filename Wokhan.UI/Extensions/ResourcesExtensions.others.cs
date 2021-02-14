using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

using Windows.ApplicationModel.Resources;

using Wokhan.Core.Extensions;

namespace Wokhan.UI.Extensions
{
    public static partial class ResourcesExtensions
    {
        static ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView();

        delegate string[] EnsureLoadersCulturesDelegate();
        static EnsureLoadersCulturesDelegate EnsureLoadersCultures = (EnsureLoadersCulturesDelegate)typeof(ResourceLoader).GetMethod("EnsureLoadersCultures", BindingFlags.Static | BindingFlags.NonPublic).CreateDelegate(typeof(EnsureLoadersCulturesDelegate));

        delegate bool FindForCultureDelegate(string culture, string resource, out string resourceValue);
        static FindForCultureDelegate FindForCulture => (FindForCultureDelegate)typeof(ResourceLoader).GetMethod("FindForCulture", BindingFlags.Instance | BindingFlags.NonPublic).CreateDelegate(typeof(FindForCultureDelegate), _resourceLoader);

        public static string Translate(this string src)
        {
            Contract.Requires(src != null);

            EnsureLoadersCultures();
            var currentLanguage = CultureInfo.CurrentUICulture.IetfLanguageTag.ToLowerInvariant();

            var value = _resourceLoader.GetString(src);
            if (value != null)
            {
                return value;
            }

            if (!currentLanguage.StartsWith("en") && FindForCulture("en", src, out value))
            {
                return value.ToPseudo();
            }

            return $"!!{src}!!";
        }
    }
}