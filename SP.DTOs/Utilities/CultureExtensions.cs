﻿using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SP.DTOs.Utilities
{
    public static class CultureExtensions
    {
        public static string GetCountryFullName(CultureInfo ci)
        {
            var name = Regex.Match(ci.DisplayName, @"(.*) \((.*)?\)");
            var cc = GetCountry(ci.Name);
            var cultureCount = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Count(c=> cc == GetCountry(c.Name));
            return cultureCount > 1
                ? $"{name.Groups[2].Value} ({name.Groups[1].Value})"
                : name.Groups[2].Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetCountry(string cultureCode)
        {
            var i = cultureCode.IndexOf('-');
            if (i == -1) { return null; }
            return cultureCode.Substring(i + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetLanguage(string cultureCode)
        {
            var i = cultureCode.IndexOf('-');
            if (i == -1) { return null; }
            return cultureCode.Substring(0, i);
        }
    }
}
