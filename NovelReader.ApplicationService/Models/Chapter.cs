using System;
using System.Collections.Generic;
using System.Text;

namespace NovelReader.ApplicationService.Models
{
    public class Chapter
    {
        public string ChapterContent { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<KeyValuePair<string, int>> ChapterWords { get; set; }
    }
}
