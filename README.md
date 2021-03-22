# BarbezDotEu.String
Generic logic for operations pertaining to text and strings (mostly - needs some cleaning).

## Contents

- [Regexes](#T-BarbezDotEu-String-Regexes 'BarbezDotEu.String.Regexes')
  - [Urls](#F-BarbezDotEu-String-Regexes-Urls 'BarbezDotEu.String.Regexes.Urls')
- [StringConstants](#T-BarbezDotEu-String-StringConstants 'BarbezDotEu.String.StringConstants')
  - [AcceptEncodingGzip](#F-BarbezDotEu-String-StringConstants-AcceptEncodingGzip 'BarbezDotEu.String.StringConstants.AcceptEncodingGzip')
  - [ApplicationGzip](#F-BarbezDotEu-String-StringConstants-ApplicationGzip 'BarbezDotEu.String.StringConstants.ApplicationGzip')
  - [FileNameCompatibleDateTime](#F-BarbezDotEu-String-StringConstants-FileNameCompatibleDateTime 'BarbezDotEu.String.StringConstants.FileNameCompatibleDateTime')
  - [Space](#F-BarbezDotEu-String-StringConstants-Space 'BarbezDotEu.String.StringConstants.Space')
  - [TLDs](#F-BarbezDotEu-String-StringConstants-TLDs 'BarbezDotEu.String.StringConstants.TLDs')
  - [TextCsv](#F-BarbezDotEu-String-StringConstants-TextCsv 'BarbezDotEu.String.StringConstants.TextCsv')
  - [UTF8](#F-BarbezDotEu-String-StringConstants-UTF8 'BarbezDotEu.String.StringConstants.UTF8')
- [StringHelper](#T-BarbezDotEu-String-StringHelper 'BarbezDotEu.String.StringHelper')
  - [AsHashSet(csvString,addOneEmptyString)](#M-BarbezDotEu-String-StringHelper-AsHashSet-System-String,System-Boolean- 'BarbezDotEu.String.StringHelper.AsHashSet(System.String,System.Boolean)')
  - [EnumerateCharactersInUnicodeUtf16String(unicodeUtf16String)](#M-BarbezDotEu-String-StringHelper-EnumerateCharactersInUnicodeUtf16String-System-String- 'BarbezDotEu.String.StringHelper.EnumerateCharactersInUnicodeUtf16String(System.String)')
  - [FindAll(needle,haystack,ignoreCase)](#M-BarbezDotEu-String-StringHelper-FindAll-System-Text-RegularExpressions-Regex,System-String,System-Boolean- 'BarbezDotEu.String.StringHelper.FindAll(System.Text.RegularExpressions.Regex,System.String,System.Boolean)')
  - [FromBase64String(base64Source,encoding)](#M-BarbezDotEu-String-StringHelper-FromBase64String-System-String,System-Text-Encoding- 'BarbezDotEu.String.StringHelper.FromBase64String(System.String,System.Text.Encoding)')
  - [GetAsTextAsync(httpContent)](#M-BarbezDotEu-String-StringHelper-GetAsTextAsync-System-Net-Http-HttpContent- 'BarbezDotEu.String.StringHelper.GetAsTextAsync(System.Net.Http.HttpContent)')
  - [GetFileName(fullFilePath)](#M-BarbezDotEu-String-StringHelper-GetFileName-System-String- 'BarbezDotEu.String.StringHelper.GetFileName(System.String)')
  - [GetRawContentString(bytes)](#M-BarbezDotEu-String-StringHelper-GetRawContentString-System-Byte[]- 'BarbezDotEu.String.StringHelper.GetRawContentString(System.Byte[])')
  - [GetValidUris(bagOfBadAndGoodUris)](#M-BarbezDotEu-String-StringHelper-GetValidUris-System-Collections-Generic-HashSet{System-String}- 'BarbezDotEu.String.StringHelper.GetValidUris(System.Collections.Generic.HashSet{System.String})')
  - [IsContentGZip(httpContent)](#M-BarbezDotEu-String-StringHelper-IsContentGZip-System-Net-Http-HttpContent- 'BarbezDotEu.String.StringHelper.IsContentGZip(System.Net.Http.HttpContent)')
  - [IsContentMediaTypeCsv(httpContent)](#M-BarbezDotEu-String-StringHelper-IsContentMediaTypeCsv-System-Net-Http-HttpContent- 'BarbezDotEu.String.StringHelper.IsContentMediaTypeCsv(System.Net.Http.HttpContent)')
  - [IsContentUtf8(httpContent)](#M-BarbezDotEu-String-StringHelper-IsContentUtf8-System-Net-Http-HttpContent- 'BarbezDotEu.String.StringHelper.IsContentUtf8(System.Net.Http.HttpContent)')
  - [KeepDuplicates(input)](#M-BarbezDotEu-String-StringHelper-KeepDuplicates-System-String[]- 'BarbezDotEu.String.StringHelper.KeepDuplicates(System.String[])')
  - [Partition(input,charsPerPartition)](#M-BarbezDotEu-String-StringHelper-Partition-System-String,System-Int32- 'BarbezDotEu.String.StringHelper.Partition(System.String,System.Int32)')
  - [RemoveDuplicateLines(input)](#M-BarbezDotEu-String-StringHelper-RemoveDuplicateLines-System-String- 'BarbezDotEu.String.StringHelper.RemoveDuplicateLines(System.String)')
  - [RemoveEnding(source,endingToRemove)](#M-BarbezDotEu-String-StringHelper-RemoveEnding-System-String,System-String- 'BarbezDotEu.String.StringHelper.RemoveEnding(System.String,System.String)')
  - [RemoveEndings(source,endingsToRemove)](#M-BarbezDotEu-String-StringHelper-RemoveEndings-System-String,System-String[]- 'BarbezDotEu.String.StringHelper.RemoveEndings(System.String,System.String[])')
  - [ReplaceEnding(source,endingToReplace,endingToReplaceBy)](#M-BarbezDotEu-String-StringHelper-ReplaceEnding-System-String,System-String,System-String- 'BarbezDotEu.String.StringHelper.ReplaceEnding(System.String,System.String,System.String)')
  - [Right()](#M-BarbezDotEu-String-StringHelper-Right-System-String,System-Int32- 'BarbezDotEu.String.StringHelper.Right(System.String,System.Int32)')
  - [ToBase64String(source,encoding)](#M-BarbezDotEu-String-StringHelper-ToBase64String-System-String,System-Text-Encoding- 'BarbezDotEu.String.StringHelper.ToBase64String(System.String,System.Text.Encoding)')
  - [ToFileNameString(dateTime,fileNameCompatibleDateTime)](#M-BarbezDotEu-String-StringHelper-ToFileNameString-System-DateTime,System-String- 'BarbezDotEu.String.StringHelper.ToFileNameString(System.DateTime,System.String)')
  - [TruncateUniqueString(value)](#M-BarbezDotEu-String-StringHelper-TruncateUniqueString-System-String- 'BarbezDotEu.String.StringHelper.TruncateUniqueString(System.String)')
- [TextFileEncodingDetector](#T-BarbezDotEu-String-TextFileEncodingDetector 'BarbezDotEu.String.TextFileEncodingDetector')
  - [DetectBOMBytes(BOMBytes)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectBOMBytes-System-Byte[]- 'BarbezDotEu.String.TextFileEncodingDetector.DetectBOMBytes(System.Byte[])')
  - [DetectTextByteArrayEncoding(TextData)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextByteArrayEncoding-System-Byte[]- 'BarbezDotEu.String.TextFileEncodingDetector.DetectTextByteArrayEncoding(System.Byte[])')
  - [DetectTextByteArrayEncoding(TextData,HasBOM)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextByteArrayEncoding-System-Byte[],System-Boolean@- 'BarbezDotEu.String.TextFileEncodingDetector.DetectTextByteArrayEncoding(System.Byte[],System.Boolean@)')
  - [DetectTextFileEncoding(InputFilename)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextFileEncoding-System-String- 'BarbezDotEu.String.TextFileEncodingDetector.DetectTextFileEncoding(System.String)')
  - [DetectTextFileEncoding(InputFileStream)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextFileEncoding-System-IO-FileStream- 'BarbezDotEu.String.TextFileEncodingDetector.DetectTextFileEncoding(System.IO.FileStream)')
  - [DetectTextFileEncoding(InputFileStream,HeuristicSampleSize,HasBOM)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextFileEncoding-System-IO-FileStream,System-Int64,System-Boolean@- 'BarbezDotEu.String.TextFileEncodingDetector.DetectTextFileEncoding(System.IO.FileStream,System.Int64,System.Boolean@)')
  - [DetectUnicodeInByteSampleByHeuristics(SampleBytes)](#M-BarbezDotEu-String-TextFileEncodingDetector-DetectUnicodeInByteSampleByHeuristics-System-Byte[]- 'BarbezDotEu.String.TextFileEncodingDetector.DetectUnicodeInByteSampleByHeuristics(System.Byte[])')
  - [GetStringFromByteArray(TextData,DefaultEncoding)](#M-BarbezDotEu-String-TextFileEncodingDetector-GetStringFromByteArray-System-Byte[],System-Text-Encoding- 'BarbezDotEu.String.TextFileEncodingDetector.GetStringFromByteArray(System.Byte[],System.Text.Encoding)')
  - [GetStringFromByteArray(TextData,DefaultEncoding,MaxHeuristicSampleSize)](#M-BarbezDotEu-String-TextFileEncodingDetector-GetStringFromByteArray-System-Byte[],System-Text-Encoding,System-Int64- 'BarbezDotEu.String.TextFileEncodingDetector.GetStringFromByteArray(System.Byte[],System.Text.Encoding,System.Int64)')

<a name='T-BarbezDotEu-String-Regexes'></a>
## Regexes `type`

A static class containing a smorgasbord of string constants.

<a name='F-BarbezDotEu-String-Regexes-Urls'></a>
### Urls `constants`

Defines a regex to get all URL "lookalike"s from a text. The purpose of this regex is to find truly valid URLs, but those
that can be used as a "vehicle" to associate their containers and domain names.

##### Remarks

Adapted from regex sourced from https://mathiasbynens.be/demo/url-regex, replaced TLDs with those from https://github.com/publicsuffix/list. Alternative: https://github.com/flipbit/tld.

<a name='T-BarbezDotEu-String-StringConstants'></a>
## StringConstants `type`

A static class containing a smorgasbord of string constants.

<a name='F-BarbezDotEu-String-StringConstants-AcceptEncodingGzip'></a>
### AcceptEncodingGzip `constants`

A constant for "gzip".

<a name='F-BarbezDotEu-String-StringConstants-ApplicationGzip'></a>
### ApplicationGzip `constants`

A constant for "application/gzip".

<a name='F-BarbezDotEu-String-StringConstants-FileNameCompatibleDateTime'></a>
### FileNameCompatibleDateTime `constants`

A constant for "yyyyMMddHmmssfffffff".

<a name='F-BarbezDotEu-String-StringConstants-Space'></a>
### Space `constants`

A constant for " ".

<a name='F-BarbezDotEu-String-StringConstants-TLDs'></a>
### TLDs `constants`

A constant containing all TLDs for anno 2020, ordered by descending place in the English alphabet, in a format ideal for use as part of a regular expression.

##### Remarks

IMPORTANT! When updating this URL list, make sure to sort TLDs from Z to A, so that COM comes before CO, and so on!

<a name='F-BarbezDotEu-String-StringConstants-TextCsv'></a>
### TextCsv `constants`

A constant for "text/csv".

<a name='F-BarbezDotEu-String-StringConstants-UTF8'></a>
### UTF8 `constants`

A constant for "utf-8".

<a name='T-BarbezDotEu-String-StringHelper'></a>
## StringHelper `type`

A static class containing a smorgasbord of string-related helper and extension methods.

<a name='M-BarbezDotEu-String-StringHelper-AsHashSet-System-String,System-Boolean-'></a>
### AsHashSet(csvString,addOneEmptyString) `method`

Returns a CSV string as a HashSet containing one element per comma-delimited entry from the CSV string.

##### Returns

HashSet containing one element per comma-delimited entry from the CSV string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| csvString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The CSV string. |
| addOneEmptyString | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Set to true to include one entry containing an empty string. Default is false. |

<a name='M-BarbezDotEu-String-StringHelper-EnumerateCharactersInUnicodeUtf16String-System-String-'></a>
### EnumerateCharactersInUnicodeUtf16String(unicodeUtf16String) `method`

Enumerates UTF16 (Unicode) characters (represented as string since it takes two old'school 'char's to represent one Unicode char sometimes).

##### Returns

The enumerated list of UTF-16/Unicode 'characters'

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| unicodeUtf16String | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Unicode (UTF-16) string to enumerate. |

<a name='M-BarbezDotEu-String-StringHelper-FindAll-System-Text-RegularExpressions-Regex,System-String,System-Boolean-'></a>
### FindAll(needle,haystack,ignoreCase) `method`

Finds one or more textual needles in a given Regex in a textual haystack, with or without taking character casing into account.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| needle | [System.Text.RegularExpressions.Regex](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.RegularExpressions.Regex 'System.Text.RegularExpressions.Regex') | The (different) piece(s) of text to find. |
| haystack | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to find the needle in. |
| ignoreCase | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Set to true to turn case-sensitivity off. |

<a name='M-BarbezDotEu-String-StringHelper-FromBase64String-System-String,System-Text-Encoding-'></a>
### FromBase64String(base64Source,encoding) `method`

Decodes a given base64-encoded string to a decoded string.

##### Returns

A decoded string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| base64Source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base64-encoded string. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The expected encoding. |

##### Remarks

Based on https://adrientorris.github.io/aspnet-core/manage-base64-encoding.html.

<a name='M-BarbezDotEu-String-StringHelper-GetAsTextAsync-System-Net-Http-HttpContent-'></a>
### GetAsTextAsync(httpContent) `method`

Converts [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') to textual string content, even if GZipped.

##### Returns

A string representation of the given [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpContent | [System.Net.Http.HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') | The [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') to convert. |

<a name='M-BarbezDotEu-String-StringHelper-GetFileName-System-String-'></a>
### GetFileName(fullFilePath) `method`

Returns only the file name as found inside the given full file path. The path must be of form C:\\Another\\One, C:/Example/Directory/Structure.txt won't work.

##### Returns

The file name as found inside the given full file path.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fullFilePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The full file path. |

<a name='M-BarbezDotEu-String-StringHelper-GetRawContentString-System-Byte[]-'></a>
### GetRawContentString(bytes) `method`

Returns a decoded content string from the given bytes.

##### Returns

Null if fail; a text if all went well.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bytes | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The bytes to convert to a string. |

<a name='M-BarbezDotEu-String-StringHelper-GetValidUris-System-Collections-Generic-HashSet{System-String}-'></a>
### GetValidUris(bagOfBadAndGoodUris) `method`

From a given bag containing good and bad URIs, returns only a list of valid URIs.

##### Returns

Only a list of valid URIs from the given bag of good and bad URIs.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bagOfBadAndGoodUris | [System.Collections.Generic.HashSet{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.HashSet 'System.Collections.Generic.HashSet{System.String}') | The bag of URIs to sift through. |

<a name='M-BarbezDotEu-String-StringHelper-IsContentGZip-System-Net-Http-HttpContent-'></a>
### IsContentGZip(httpContent) `method`

Checks if [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') is GZipped and if so, returns true.

##### Returns

True if GZipped.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpContent | [System.Net.Http.HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') | The [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') to check. |

<a name='M-BarbezDotEu-String-StringHelper-IsContentMediaTypeCsv-System-Net-Http-HttpContent-'></a>
### IsContentMediaTypeCsv(httpContent) `method`

Checks if [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') has content that is CSV and if so, returns true.

##### Returns

True if the content is CSV.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpContent | [System.Net.Http.HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') | The [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') to check. |

<a name='M-BarbezDotEu-String-StringHelper-IsContentUtf8-System-Net-Http-HttpContent-'></a>
### IsContentUtf8(httpContent) `method`

Checks if [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') is UTF8 and if so, returns true.

##### Returns

True if the content is UTF8.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpContent | [System.Net.Http.HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') | The [HttpContent](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpContent 'System.Net.Http.HttpContent') to check. |

<a name='M-BarbezDotEu-String-StringHelper-KeepDuplicates-System-String[]-'></a>
### KeepDuplicates(input) `method`

From a given input string, retains only the first occurences of all duplicate entries.

##### Returns

Array of first occurences of all duplicate entries.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The input string to distill duplicates out of. |

<a name='M-BarbezDotEu-String-StringHelper-Partition-System-String,System-Int32-'></a>
### Partition(input,charsPerPartition) `method`

Splits a given string up into even blocks (except for the last item in the list when the input string was not long enough).

##### Returns

A [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') containing the split up partitions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to split up. |
| charsPerPartition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The number of characters to split up per item (partition) in the resulting list. |

<a name='M-BarbezDotEu-String-StringHelper-RemoveDuplicateLines-System-String-'></a>
### RemoveDuplicateLines(input) `method`

Removes duplicate lines from a given string containing newlines.

##### Returns

The inputted string without the duplicate lines.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A newlines-containing input string to remove duplicate lines from. |

<a name='M-BarbezDotEu-String-StringHelper-RemoveEnding-System-String,System-String-'></a>
### RemoveEnding(source,endingToRemove) `method`

Removes the ending of a given string, if matching the given ending.

##### Returns

The source string without the undesired ending.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to remove the ending from, if applicable. |
| endingToRemove | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ending to remove from the source string, if applicable. |

<a name='M-BarbezDotEu-String-StringHelper-RemoveEndings-System-String,System-String[]-'></a>
### RemoveEndings(source,endingsToRemove) `method`

Removes the endings of a given string, if matching any of the given endings.

##### Returns

The source string without the undesired endings.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to remove the endings from, if applicable. |
| endingsToRemove | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The endings to remove from the source string, if applicable. |

<a name='M-BarbezDotEu-String-StringHelper-ReplaceEnding-System-String,System-String,System-String-'></a>
### ReplaceEnding(source,endingToReplace,endingToReplaceBy) `method`

Replaces the ending of a given string, if matching the given ending to replace.

##### Returns

The source string of which to replace the ending with, if applicable.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to replace the ending from, if applicable. |
| endingToReplace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ending to replace of the source string, if applicable. |
| endingToReplaceBy | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ending to replace the ending of the source string by. |

<a name='M-BarbezDotEu-String-StringHelper-Right-System-String,System-Int32-'></a>
### Right() `method`

Gets a substring of a specified number of characters starting from the right.

##### Parameters

This method has no parameters.

##### Remarks

Source: https://www.dotnetperls.com/right.

<a name='M-BarbezDotEu-String-StringHelper-ToBase64String-System-String,System-Text-Encoding-'></a>
### ToBase64String(source,encoding) `method`

Encodes a given string as base64.

##### Returns

The given string encoded as base64.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to encode as base64. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The expected encoding. |

##### Remarks

Based on https://adrientorris.github.io/aspnet-core/manage-base64-encoding.html.

<a name='M-BarbezDotEu-String-StringHelper-ToFileNameString-System-DateTime,System-String-'></a>
### ToFileNameString(dateTime,fileNameCompatibleDateTime) `method`

Returns a fine-grained, probably unique, string representation of a given date time.

##### Returns

The string representation of the given date and time.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateTime | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | The [DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') to convert to string. |
| fileNameCompatibleDateTime | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A standard or custom date and time format string. |

<a name='M-BarbezDotEu-String-StringHelper-TruncateUniqueString-System-String-'></a>
### TruncateUniqueString(value) `method`

Truncates a string to the rightmost X characters so that the most
valuable information is retained (e.g. by truncating FQDNs
and emailaddresses this way, we retain the top-level domains, i.e.
the most valuable information essentially, is retained by truncating
from the right.

##### Returns

The truncated string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string to truncate. |

##### Remarks

This method is intended to be used on properties that are defined as unique, and thus have
a maximum column length of 450 characters in MSSQL/EF Core environments as ours.

<a name='T-BarbezDotEu-String-TextFileEncodingDetector'></a>
## TextFileEncodingDetector `type`

Simple class to handle text file encoding woes (in a primarily English-speaking tech world).

##### Remarks

Copyright Tao Klerks, 2010-2012, tao @klerks.biz
Licensed under the modified BSD license (see inside class).

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectBOMBytes-System-Byte[]-'></a>
### DetectBOMBytes(BOMBytes) `method`

Detects BOM bytes.

##### Returns

The [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| BOMBytes | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The BOM bytes array. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextByteArrayEncoding-System-Byte[]-'></a>
### DetectTextByteArrayEncoding(TextData) `method`

Detects file encoding of binary text data.

##### Returns

The text file's [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| TextData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The binary text data to detect encoding for. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextByteArrayEncoding-System-Byte[],System-Boolean@-'></a>
### DetectTextByteArrayEncoding(TextData,HasBOM) `method`

Detects file encoding of binary text data.

##### Returns

The text file's [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| TextData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The binary text data to detect encoding for. |
| HasBOM | [System.Boolean@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean@ 'System.Boolean@') | Set to true if the file has BOM. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextFileEncoding-System-String-'></a>
### DetectTextFileEncoding(InputFilename) `method`

Detects file encoding of a text file.

##### Returns

The text file's [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| InputFilename | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text file to detect encoding for. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextFileEncoding-System-IO-FileStream-'></a>
### DetectTextFileEncoding(InputFileStream) `method`

Detects file encoding of a text file.

##### Returns

The text file's [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| InputFileStream | [System.IO.FileStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.FileStream 'System.IO.FileStream') | The text file to detect encoding for. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectTextFileEncoding-System-IO-FileStream,System-Int64,System-Boolean@-'></a>
### DetectTextFileEncoding(InputFileStream,HeuristicSampleSize,HasBOM) `method`

Detects file encoding of a text file.

##### Returns

The text file's [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| InputFileStream | [System.IO.FileStream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.FileStream 'System.IO.FileStream') | The text file to detect encoding for. |
| HeuristicSampleSize | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The sample size. |
| HasBOM | [System.Boolean@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean@ 'System.Boolean@') | Set to true if the file has BOM. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-DetectUnicodeInByteSampleByHeuristics-System-Byte[]-'></a>
### DetectUnicodeInByteSampleByHeuristics(SampleBytes) `method`

Detects Unicode in sample bytes by heuristics.

##### Returns

The [Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| SampleBytes | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The sample byte array. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-GetStringFromByteArray-System-Byte[],System-Text-Encoding-'></a>
### GetStringFromByteArray(TextData,DefaultEncoding) `method`

Gets a string from a byte array.

##### Returns

The string as retrieved from the byte array.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| TextData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The binary text data to detect encoding for. |
| DefaultEncoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding using which to interpret the byte array. |

<a name='M-BarbezDotEu-String-TextFileEncodingDetector-GetStringFromByteArray-System-Byte[],System-Text-Encoding,System-Int64-'></a>
### GetStringFromByteArray(TextData,DefaultEncoding,MaxHeuristicSampleSize) `method`

Gets a string from a byte array.

##### Returns

The string as retrieved from the byte array.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| TextData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The binary text data to detect encoding for. |
| DefaultEncoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding using which to interpret the byte array. |
| MaxHeuristicSampleSize | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The maximum sample size. |

## Author
www.barbez.eu
