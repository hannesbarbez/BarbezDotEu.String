// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System.Linq;
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
            var textWithDuplicates = "𝓵𝓽𝓽𝓽𝓽𝓪𝓪𝓲𝓪𝓸𝓪𝓵𝓪𝓮𝓪𝓽𝓽𝓽𝓪𝓵";
            var ExpectedTextWithoutDuplicates = "𝓵𝓽𝓪𝓲𝓸𝓮";

            // Retain one UTF-16 character only once
            var textWithoutDuplicates = string.Join(null, StringHelper.EnumerateCharactersInUnicodeUtf16String(textWithDuplicates).Distinct().ToArray());

            Assert.AreEqual(ExpectedTextWithoutDuplicates, textWithoutDuplicates, false);
        }

        [TestMethod]
        public void ReturningDuplicatesWorks()
        {
            var duplicateValue = "a";
            var nonDuplicateValue = "b";
            var input = new string[] { duplicateValue, duplicateValue, nonDuplicateValue, "" };
            var duplicates = StringHelper.KeepDuplicates(input);
            Assert.IsTrue(duplicates.Contains(duplicateValue));
            Assert.IsTrue(!duplicates.Contains(nonDuplicateValue));
        }
    }
}
