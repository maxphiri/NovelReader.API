using System;
using System.Collections.Generic;
using System.Text;

namespace NovelReader.ApplicationService.Helpers
{
    public interface IStringManager
    {
        string[] SplitText(string content, string splitOption);

        string GetNovelAsText(string content);

        string[] RemoveSpecialChars(string content);

        Dictionary<string, int> WordsCounter(string[] fullWordsList);
    }
}
