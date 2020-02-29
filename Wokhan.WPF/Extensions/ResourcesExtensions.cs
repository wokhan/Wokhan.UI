using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Wokhan.Core.Extensions;
using System.Diagnostics.Contracts;

namespace Wokhan.UI.Extensions
{
    public static partial class ResourcesExtensions
    {
        private static ResourceManager resourceManager = new ResourceManager("Resources", Assembly.GetCallingAssembly());

        public static string Translate(this string src)
        {
            Contract.Requires(src != null);

            var res = resourceManager.GetString(src);
            if (res == null)
            {
                res = resourceManager.GetString(src, CultureInfo.InvariantCulture)?.ToPseudo();
            }

            return res ?? $"!!{src}!!";
        }
    }
}
