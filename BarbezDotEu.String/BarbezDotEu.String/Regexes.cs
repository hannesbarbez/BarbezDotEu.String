// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Text.RegularExpressions;

namespace BarbezDotEu.String
{
    /// <summary>
    /// A static class containing a smorgasbord of string constants.
    /// </summary>
    public static class Regexes
    {
        /// <summary>
        /// Defines a regex to get all URL "lookalike"s from a text. The purpose of this regex is to find truly valid URLs, but those
        /// that can be used as a "vehicle" to associate their containers and domain names.
        /// </summary>
        /// <remarks>
        /// Adapted from regex sourced from https://mathiasbynens.be/demo/url-regex, replaced TLDs with those from https://github.com/publicsuffix/list. Alternative: https://github.com/flipbit/tld.
        /// </remarks>
        public static readonly Regex Urls = new(@"(((http|ftp|https):\/{2})+(([0-9a-z_-]+\.)+(" + StringConstants.TLDs + @")(:[0-9]+)?((\/([~0-9a-zA-Z\#\+\%@\.\/_-]+))?(\?[0-9a-zA-Z\+\%@\/&\[\];=_-]+)?)?))", RegexOptions.Compiled);

        /// <summary>
        /// Defines a regex to get all non-alpha numericalls from a text.
        /// </summary>
        public static readonly Regex NonAlphaNumericals = new("[^a-zA-Z0-9]", RegexOptions.Compiled);
    }
}
