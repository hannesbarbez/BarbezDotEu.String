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
    public static class StringHelper
    {
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
            if (uniques.Count() == input.Length)
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
            return value.Substring(value.Length - length);
        }

        /// <summary>
        /// Returns a fine-grained, probably unique, string representation of a given date time.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert to string.</param>
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
        /// <param name="parts">The number of characters to split up per item (partition) in the resulting list.</param>
        /// <returns>A <see cref="List{T}"/> containing the split up partitions.</returns>
        public static List<string> Partition(this string input, int charsPerPartition)
        {
            var parts = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                if (parts.Count() == 0 || (parts[parts.Count() - 1].Length == charsPerPartition))
                {
                    parts.Add(input[i].ToString());
                }
                else if (parts[parts.Count() - 1].Length != charsPerPartition)
                {
                    parts[parts.Count() - 1] += input[i].ToString();
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
                yield return (string)enumerator.GetTextElement();
        }
    }
}
