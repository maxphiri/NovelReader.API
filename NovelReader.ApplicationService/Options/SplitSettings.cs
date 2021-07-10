using System;
using System.Collections.Generic;
using System.Text;

namespace NovelReader.ApplicationService.Options
{
    public class SplitSettings
    {
        public string TextStartPoint { get; set; }
        
        public string SplitOption { get; set; }
        
        public string SubStringSplitOption { get; set; }
        
        public string TextEndPoint { get; set; }

        public int WordSize { get; set; }

        public string SourceFilePath { get; set; }
    }
}
