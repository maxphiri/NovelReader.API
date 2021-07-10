using System;
using System.Collections.Generic;
using System.Text;

namespace NovelReader.ApplicationService.Models
{
    public class Book
    {
        public string BookContent { get; set; }

        public int TotalCount { get; set; }

        public List<Chapter> Chapters {get; set; }

        public IEnumerable<KeyValuePair<string, int>> BookWords { get; set; }
    }
}
