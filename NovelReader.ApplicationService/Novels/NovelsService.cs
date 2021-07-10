using Microsoft.Extensions.Options;
using NovelReader.ApplicationService.Helpers;
using NovelReader.ApplicationService.Models;
using NovelReader.ApplicationService.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NovelReader.ApplicationService.Novels
{
    public class NovelsService : INovelsService
    {
        private readonly IStringManager _stringManager;
        private readonly SplitSettings _splitSettings;
        public NovelsService(IStringManager stringManager, IOptions<SplitSettings> splitSettings)
        {
            _stringManager = stringManager;
            _splitSettings = splitSettings.Value;
        }

        public async Task<Novel> ReadWords(int size, bool isDesc)
        {
            var novel = new Novel();

            using var fs = new FileStream(_splitSettings.SourceFilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            var source = await sr.ReadToEndAsync();

            var novelContent = _stringManager.GetNovelAsText(source);
            var books = _stringManager.SplitText(novelContent, _splitSettings.SplitOption);
            var fullWordsList = _stringManager.RemoveSpecialChars(novelContent);
            novel.TotalCount = fullWordsList.Length;

            var wordCounts = _stringManager.WordsCounter(fullWordsList);

            if (isDesc)
            {
                novel.NovelWords = wordCounts.OrderByDescending(key => key.Value).Take(size);
                return novel;
            }

            novel.NovelWords = wordCounts.OrderBy(key => key.Value).Take(size);
            return novel;
        }

        public async Task<Novel> ReadWordsPerBook(int size, bool isDesc)
        {
            var novel = new Novel();

            using var fs = new FileStream(_splitSettings.SourceFilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            var source = await sr.ReadToEndAsync();

            var novelContent = _stringManager.GetNovelAsText(source);
            var books = _stringManager.SplitText(novelContent, _splitSettings.SplitOption);
            
            foreach (string bookText  in books)
            {
                var book = new Book();
                var bookWordsList = _stringManager.RemoveSpecialChars(bookText);
                book.TotalCount = bookWordsList.Length;
                book.BookWords = _stringManager.WordsCounter(bookWordsList).OrderByDescending(key => key.Value).Take(size);

                var chapters = _stringManager.SplitText(bookText, _splitSettings.SubStringSplitOption);
                foreach (var chapterText in chapters)
                {
                    var chapter = new Chapter();
                    var chapterWordsList = _stringManager.RemoveSpecialChars(chapterText);
                    chapter.TotalCount = chapterWordsList.Length;
                    chapter.ChapterWords = _stringManager.WordsCounter(chapterWordsList).OrderByDescending(key => key.Value).Take(size);
                    book.Chapters.Add(chapter);
                }

                novel.Books.Add(book);
            }

            return novel;
        }
    }
}
