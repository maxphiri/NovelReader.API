using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NovelReader.API.Model
{
    public class PageRequest
    {
        [DisplayName("Size")]
        public int Size { get; set; }

        [DisplayName("Is Descending")]
        public bool IsDesc { get; set; }
    }
}
