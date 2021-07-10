using Microsoft.Extensions.Options;
using NovelReader.ApplicationService.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NovelReader.ApplicationService.Helpers
{
    public class StringManager: IStringManager
    {

        private readonly SplitSettings _splitSettings;

        public StringManager(IOptions<SplitSettings> splitSettings)
        {
            _splitSettings = splitSettings.Value;
        }

        public string[] SplitText(string content, string splitOption)
        {
            if (string.IsNullOrEmpty(content.Trim()))
                return null;

            return Regex.Split(content, splitOption);
        }

        public string GetNovelAsText(string content)
        {
            if (!content.Contains(_splitSettings.TextStartPoint)
                || !content.Contains(_splitSettings.TextEndPoint))
            {
                return string.Empty;
            }

            var novelText = content.Split(_splitSettings.TextStartPoint).Last()
                                   .Split(_splitSettings.TextEndPoint).First();

            return novelText.Trim();
        }

        public string[] RemoveSpecialChars(string content)
        {
            if (string.IsNullOrEmpty(content.Trim()))
                return null;

            var delimeters = new List<char> { '\r', '\n', ' ' };

            Regex specialCharRegex = new Regex("(?:[^a-z ]|(?<=['\"])s)",
                                                RegexOptions.IgnoreCase |
                                                RegexOptions.CultureInvariant |
                                                RegexOptions.Compiled);

            return specialCharRegex.Replace(content, " ")
                    .ToLower()
                    .Split(delimeters.ToArray(),
                            StringSplitOptions.RemoveEmptyEntries);
        }

        public Dictionary<string, int> WordsCounter(string[] fullWordsList)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var word in fullWordsList)
            {
                var inputWord = word.Trim();

                if (inputWord.Length >= _splitSettings.WordSize)
                {
                    int count;
                    if (wordCounts.TryGetValue(inputWord, out count))
                    {
                        wordCounts[inputWord] = count + 1;
                        continue;
                    }
                 
                    wordCounts.Add(inputWord, 1);
                }
            }

            return wordCounts;
        }
    }
}
