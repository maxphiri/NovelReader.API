using System;
using System.Collections.Generic;
using System.Text;

namespace NovelReader.ApplicationService.Models
{
    public class Novel
    {
        public string NovelContent { get; set; }

        public int TotalCount { get; set; }

        public List<Book> Books {get; set;}

        public long Duration { get; set; }

        public IEnumerable<KeyValuePair<string, int>> NovelWords { get; set; }
    }
}
