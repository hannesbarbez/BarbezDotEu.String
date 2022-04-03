// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BarbezDotEu.String
{
    /// <summary>
    /// A static class containing a smorgasbord of string-related helper and extension methods.
    /// </summary>
    public static class StringHelper
    {
        private static readonly string[] DASHABLES = new string[] { " " };
        private static readonly string[] REMOVABLES = new string[] { ",", "(", ")" };

        /// <summary>
        /// From a given bag containing good and bad URIs, returns only a list of valid URIs.
        /// </summary>
        /// <param name="bagOfBadAndGoodUris">The bag of URIs to sift through.</param>
        /// <returns>Only a list of valid URIs from the given bag of good and bad URIs.</returns>
        public static HashSet<string> GetValidUris(this HashSet<string> bagOfBadAndGoodUris)
        {
            var uriLookalikes = new HashSet<string>();
            foreach (var link in bagOfBadAndGoodUris)
            {
                var url = link;
                if (!url.StartsWith("http"))
                {
                    url = $"http://{url}";
                }

                if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _))
                {
                    uriLookalikes.Add(url);
                }
            }

            var uriLookalikesText = string.Join(StringConstants.Space, uriLookalikes.ToArray());
            var validUrls = Regexes.Urls.Matches(uriLookalikesText).Select(x => x.Value);
            return validUrls.ToHashSet();
        }

        /// <summary>
        /// Encodes a given string as base64.
        /// </summary>
        /// <param name="source">The string to encode as base64.</param>
        /// <param name="encoding">The expected encoding.</param>
        /// <returns>The given string encoded as base64.</returns>
        /// <remarks>Based on https://adrientorris.github.io/aspnet-core/manage-base64-encoding.html.</remarks>
        public static string ToBase64String(this string source, Encoding encoding)
        {
            byte[] encodedBytes = encoding.GetBytes(source);
            return Convert.ToBase64String(encodedBytes);
        }

        /// <summary>
        /// Decodes a given base64-encoded string to a decoded string.
        /// </summary>
        /// <param name="base64Source">The base64-encoded string.</param>
        /// <param name="encoding">The expected encoding.</param>
        /// <returns>A decoded string.</returns>
        /// <remarks>Based on https://adrientorris.github.io/aspnet-core/manage-base64-encoding.html.</remarks>
        public static string FromBase64String(this string base64Source, Encoding encoding)
        {
            byte[] decodedBytes = Convert.FromBase64String(base64Source);
            return encoding.GetString(decodedBytes);
        }

        /// <summary>
        /// Removes the ending of a given string, if matching the given ending.
        /// </summary>
        /// <param name="source">The string to remove the ending from, if applicable.</param>
        /// <param name="endingToRemove">The ending to remove from the source string, if applicable.</param>
        /// <returns>The source string without the undesired ending.</returns>
        public static string RemoveEnding(this string source, string endingToRemove)
        {
            if (source.EndsWith(endingToRemove))
            {
                var index = source.LastIndexOf(endingToRemove);
                source = source.Substring(0, index);
            }

            return source;
        }

        /// <summary>
        /// Removes the endings of a given string, if matching any of the given endings.
        /// </summary>
        /// <param name="source">The string to remove the endings from, if applicable.</param>
        /// <param name="endingsToRemove">The endings to remove from the source string, if applicable.</param>
        /// <returns>The source string without the undesired endings.</returns>
        public static string RemoveEndings(this string source, string[] endingsToRemove)
        {
            foreach (var ending in endingsToRemove)
            {
                source = source.RemoveEnding(ending);
            }

            return source;
        }

        /// <summary>
        /// Replaces the ending of a given string, if matching the given ending to replace.
        /// </summary>
        /// <param name="source">The string to replace the ending from, if applicable.</param>
        /// <param name="endingToReplace">The ending to replace of the source string, if applicable.</param>
        /// <param name="endingToReplaceBy">The ending to replace the ending of the source string by.</param>
        /// <returns>The source string of which to replace the ending with, if applicable.</returns>
        public static string ReplaceEnding(this string source, string endingToReplace, string endingToReplaceBy)
        {
            if (source.EndsWith(endingToReplace))
            {
                var index = source.LastIndexOf(endingToReplace);
                source = source.Substring(0, index);
                source = $"{source}{endingToReplaceBy}";
            }

            return source;
        }

        /// <summary>
        /// Returns a CSV string as a HashSet containing one element per comma-delimited entry from the CSV string.
        /// </summary>
        /// <param name="csvString">The CSV string.</param>
        /// <param name="addOneEmptyString">Set to true to include one entry containing an empty string. Default is false.</param>
        /// <returns>HashSet containing one element per comma-delimited entry from the CSV string.</returns>
        public static HashSet<string> AsHashSet(this string csvString, bool addOneEmptyString = false)
        {
            var results = new HashSet<string>();
            if (addOneEmptyString)
                results.Add(string.Empty);

            if (!string.IsNullOrWhiteSpace(csvString))
            {
                var split = csvString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                results.UnionWith(split);
            }

            return results;
        }

        /// <summary>
        /// Converts <see cref="HttpContent"/> to textual string content, even if GZipped.
        /// </summary>
        /// <param name="httpContent">The <see cref="HttpContent"/> to convert.</param>
        /// <returns>A string representation of the given <see cref="HttpContent"/>.</returns>
        public static async Task<string> GetAsTextAsync(this HttpContent httpContent)
        {
            var text = string.Empty;
            if (httpContent.IsContentGZip())
            {
                using var outputStream = new MemoryStream();
                var input = httpContent.ReadAsStreamAsync().Result;
                using (var inputStream = new GZipStream(input, CompressionMode.Decompress))
                {
                    inputStream.CopyTo(outputStream); // Decompress input to output.
                    outputStream.Flush(); // Make sure all bytes are written.
                }
                outputStream.Position = 0;

                if (httpContent.IsContentUtf8())
                {
                    text = Encoding.UTF8.GetString(outputStream.ToArray());
                }
                else
                {
                    using StreamReader streamReader = new StreamReader(outputStream);
                    text = streamReader.ReadToEnd();
                }
            }
            else
            {
                text = await httpContent.ReadAsStringAsync();
            }
            return text;
        }

        /// <summary>
        /// Removes duplicate lines from a given string containing newlines.
        /// </summary>
        /// <param name="input">A newlines-containing input string to remove duplicate lines from.</param>
        /// <returns>The inputted string without the duplicate lines.</returns>
        public static string RemoveDuplicateLines(this string input)
        {
            var splitLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var hashedLines = new HashSet<string>(splitLines);
            var result = string.Join(Environment.NewLine, hashedLines);
            return result;
        }

        /// <summary>
        /// From a given input string, retains only the first occurences of all duplicate entries.
        /// </summary>
        /// <param name="input">The input string to distill duplicates out of.</param>
        /// <returns>Array of first occurences of all duplicate entries.</returns>
        public static IEnumerable<string> KeepDuplicates(this string[] input)
        {
            HashSet<string> uniques = new HashSet<string>(input);
            if (uniques.Count == input.Length)
            {
                return new List<string>();
            }

            ConcurrentBag<string> results = new ConcurrentBag<string>();
            Parallel.ForEach(uniques, x =>
            {
                if (input.Count(y => string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase)) > 1)
                {
                    results.Add(x);
                }
            });

            return results.AsEnumerable();
        }

        /// <summary>
        /// Returns only the file name as found inside the given full file path. The path must be of form C:\\Another\\One, C:/Example/Directory/Structure.txt won't work.
        /// </summary>
        /// <param name="fullFilePath">The full file path.</param>
        /// <returns>The file name as found inside the given full file path.</returns>
        public static string GetFileName(this string fullFilePath)
        {
            var result = Regex.Match(fullFilePath, @".*\\([^\\]+$)").Groups[1].Value;
            return result;
        }

        /// <summary>
        /// Finds one or more textual needles in a given Regex in a textual haystack, with or without taking character casing into account.
        /// </summary>
        /// <param name="needle">The (different) piece(s) of text to find.</param>
        /// <param name="haystack">The text to find the needle in.</param>
        /// <param name="ignoreCase">Set to true to turn case-sensitivity off.</param>
        /// <returns>All words found inside of the given haystack (i.e. text).</returns>
        public static HashSet<string> FindAll(Regex needle, string haystack, bool ignoreCase)
        {
            if (ignoreCase)
                return new HashSet<string>(needle
                    .Matches(haystack.ToLowerInvariant())
                    .Select(x => x.ToString()));

            return new HashSet<string>(needle
                    .Matches(haystack)
                    .Select(x => x.ToString()));
        }

        /// <summary>
        /// Finds one or more textual needles in a given Regex in a textual haystack,
        /// with or without taking character casing into account. Nex to this, returns the word count per hit.
        /// </summary>
        /// <param name="needle">The (different) piece(s) of text to find.</param>
        /// <param name="haystack">The text to find the needle in.</param>
        /// <param name="ignoreCase">Set to true to turn case-sensitivity off.</param>
        /// <returns>All words found inside of the given haystack (i.e. text), together with how many times each word was found inside the text.s</returns>
        public static IDictionary<string, long> FindAndCountAll(Regex needle, string haystack, bool ignoreCase)
        {
            var nonUniqueWords = ignoreCase
                ? needle.Matches(haystack.ToLowerInvariant()).Select(x => x.ToString())
                : needle.Matches(haystack).Select(x => x.ToString());

            var uniqueWords = new HashSet<string>(nonUniqueWords);
            var result = new ConcurrentDictionary<string, long>();
            Parallel.ForEach(uniqueWords, uniqueWord =>
            {
                var occurrences = from occurence in nonUniqueWords
                                  where occurence.Equals(uniqueWord, StringComparison.InvariantCultureIgnoreCase)
                                  select occurence;

                result.TryAdd(uniqueWord, occurrences.LongCount());
            });

            return result;
        }

        /// <summary>
        /// Truncates a string to the rightmost X characters so that the most
        /// valuable information is retained (e.g. by truncating FQDNs
        /// and emailaddresses this way, we retain the top-level domains, i.e.
        /// the most valuable information essentially, is retained by truncating
        /// from the right.
        /// </summary>
        /// <remarks>
        /// This method is intended to be used on properties that are defined as unique, and thus have
        /// a maximum column length of 450 characters in MSSQL/EF Core environments as ours.
        /// </remarks>
        /// <param name="value">The string to truncate.</param>
        /// <returns>The truncated string.</returns>
        public static string TruncateUniqueString(this string value)
        {
            var result = value;
            if (result.Length > StringConstants.UniqueStringMaxCharacterCount)
            {
                result = result.Right(450);
            }

            return result;
        }

        /// <summary>
        /// Gets a substring of a specified number of characters starting from the right.
        /// </summary>
        /// <remarks>
        /// Source: https://www.dotnetperls.com/right.
        /// </remarks>
        public static string Right(this string value, int length)
        {
            return value[^length..];
        }

        /// <summary>
        /// Returns a fine-grained, probably unique, string representation of a given date time.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert to string.</param>
        /// <param name="fileNameCompatibleDateTime">A standard or custom date and time format string.</param>
        /// <returns>The string representation of the given date and time.</returns>
        public static string ToFileNameString(this DateTime dateTime, string fileNameCompatibleDateTime)
        {
            return dateTime.ToString(fileNameCompatibleDateTime);
        }

        /// <summary>
        /// Returns a decoded content string from the given bytes.
        /// </summary>
        /// <param name="bytes">The bytes to convert to a string.</param>
        /// <returns>Null if fail; a text if all went well.</returns>
        public static string GetRawContentString(byte[] bytes)
        {
            if (bytes.Any())
            {
                // Best-effort.
                try
                {
                    var content = TextFileEncodingDetector.GetStringFromByteArray(bytes, Encoding.Default);
                    return content;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if <see cref="HttpContent"/> has content that is CSV and if so, returns true.
        /// </summary>
        /// <param name="httpContent">The <see cref="HttpContent"/> to check.</param>
        /// <returns>True if the content is CSV.</returns>
        public static bool IsContentMediaTypeCsv(this HttpContent httpContent)
        {
            return string.Equals(httpContent.Headers.ContentType.MediaType, StringConstants.TextCsv, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Checks if <see cref="HttpContent"/> is UTF8 and if so, returns true.
        /// </summary>
        /// <param name="httpContent">The <see cref="HttpContent"/> to check.</param>
        /// <returns>True if the content is UTF8.</returns>
        public static bool IsContentUtf8(this HttpContent httpContent)
        {
            return string.Equals(httpContent.Headers.ContentType.CharSet, StringConstants.UTF8, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Checks if <see cref="HttpContent"/> is GZipped and if so, returns true.
        /// </summary>
        /// <param name="httpContent">The <see cref="HttpContent"/> to check.</param>
        /// <returns>True if GZipped.</returns>
        public static bool IsContentGZip(this HttpContent httpContent)
        {
            return httpContent.Headers.ContentEncoding.Any(x => string.Equals(x, StringConstants.AcceptEncodingGzip, StringComparison.InvariantCultureIgnoreCase))
                || (httpContent.Headers.ContentType != null && string.Equals(httpContent.Headers.ContentType.MediaType, StringConstants.ApplicationGzip, StringComparison.InvariantCultureIgnoreCase));
        }
        /// <summary>
        /// Splits a given string up into even blocks (except for the last item in the list when the input string was not long enough).
        /// </summary>
        /// <param name="input">The string to split up.</param>
        /// <param name="charsPerPartition">The number of characters to split up per item (partition) in the resulting list.</param>
        /// <returns>A <see cref="List{T}"/> containing the split up partitions.</returns>
        public static List<string> Partition(this string input, int charsPerPartition)
        {
            var parts = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                // "^1" == "parts.Count - 1"
                if (parts.Count == 0 || (parts[^1].Length == charsPerPartition))
                {
                    parts.Add(input[i].ToString());
                }
                else if (parts[^1].Length != charsPerPartition)
                {
                    parts[^1] += input[i].ToString();
                }
            }
            return parts;
        }

        /// <summary>
        /// Enumerates UTF16 (Unicode) characters (represented as string since it takes two old'school 'char's to represent one Unicode char sometimes).
        /// </summary>
        /// <param name="unicodeUtf16String">The Unicode (UTF-16) string to enumerate.</param>
        /// <returns>The enumerated list of UTF-16/Unicode 'characters'</returns>
        public static IEnumerable<string> EnumerateCharactersInUnicodeUtf16String(string unicodeUtf16String)
        {
            var enumerator = StringInfo.GetTextElementEnumerator(unicodeUtf16String.Normalize());
            while (enumerator.MoveNext())
                yield return enumerator.GetTextElement();
        }

        /// <summary>
        /// Based on a given class that is a DTO or entity representing an equivalent table in a database, return what would be the table's name.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The name of the equivalent table in database for the given DTO.</returns>
        public static string GetAsDatabaseTableName(this Type type)
        {
            var entity = type.Name
                .ReplaceEnding("y", "ie")
                .ReplaceEnding("s", "se");

            return $"{entity}s";
        }

        /// <summary>
        /// Adds spaces to camel-cased enumeration string values.
        /// </summary>
        /// <param name="model">The camel-cased enumeration string value to add spaces to.</param>
        /// <returns>A string representation containing spaces for the given enumeration value.</returns>
        public static string AddSpaces(this Enum model)
        {
            var camelCaseStringToConvert = model.ToString();
            return camelCaseStringToConvert.AddSpaces();
        }

        /// <summary>
        /// Adds spaces to a camel-cased string.
        /// </summary>
        /// <param name="camelCaseStringToConvert">The camel-cased string value to add spaces to.</param>
        /// <returns>A string representation containing spaces for the given enumeration value.</returns>
        public static string AddSpaces(this string camelCaseStringToConvert)
        {
            return Regex.Replace(camelCaseStringToConvert, "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }

        /// <summary>
        /// Gets the enumeration for a given string value representing the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="model">The string value to translate into the enumeration.</param>
        /// <returns>An enumeration representation of the given string.</returns>
        /// <remarks>Based on https://stackoverflow.com/a/52588251</remarks>
        public static T ConvertToEnum<T>(this string model)
        {
            var newModel = model.MakeEnumCompatible();

            if (!Enum.IsDefined(typeof(T), newModel))
            {
                // For enums, default returns 0 - all enums therefor should ideally have a 0 defined.
                return default;
            }

            return (T)Enum.Parse(typeof(T), newModel, true);
        }

        /// <summary>
        /// Moulds a given string into a string that is a form parsable into any enumeration.
        /// </summary>
        /// <param name="model">The string to parse.</param>
        /// <returns>A string compatible with enumeration parsing from string.</returns>
        /// <remarks>Based on https://stackoverflow.com/a/52588251</remarks>
        public static string MakeEnumCompatible(this string model)
        {
            List<string> invalidCharacters = new();
            invalidCharacters.ForEach(x => model = model.Replace(x, string.Empty));
            var charsToRemove = Regexes.NonAlphaNumericals
                .Matches(model)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            if (!charsToRemove.Any())
            {
                return model;
            }

            invalidCharacters.AddRange(charsToRemove);
            invalidCharacters.ForEach(x => model = model.Replace(x, string.Empty));
            return model;
        }

        /// <summary>
        /// Converts a string to its escaped representation after all spaces have been replaced with dashes.
        /// The result is converted to lowercase, after which it is returned.
        /// </summary>
        /// <param name="nonUrlFriendlyString">The URL to convert.</param>
        /// <returns>The converted URL.</returns>
        public static string AsUrlFriendly(this string nonUrlFriendlyString)
        {
            foreach (var dashable in DASHABLES)
                nonUrlFriendlyString = nonUrlFriendlyString.Replace(dashable, "-");

            foreach (var removable in REMOVABLES)
                nonUrlFriendlyString = nonUrlFriendlyString.Replace(removable, string.Empty);

            return Uri.EscapeDataString(nonUrlFriendlyString.ToLowerInvariant());
        }
    }
}
