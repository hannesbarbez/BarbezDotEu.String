// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbezDotEu.String.Testing.Unit
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void RemovingDuplicateUtf16CharactersWorks()
        {
            // Assuming a file with duplicate characters:
            var textFilePath = "C:/Users/hanne/Desktop/headers.txt";
            var textWithDuplicateCharacters = File.ReadAllText(textFilePath, Encoding.UTF8);

            // Retain one UTF-16 character only once
            var textWithoutDuplicateCharacters = string.Join(null, StringHelper.EnumerateCharactersInUnicodeUtf16String(textWithDuplicateCharacters).Distinct().ToArray());

            // Overwrite file with cleaned up string:
            File.WriteAllText(textFilePath, textWithoutDuplicateCharacters, Encoding.UTF8);

            Assert.AreNotEqual(textWithDuplicateCharacters, textWithoutDuplicateCharacters, false);
        }
    }
}
