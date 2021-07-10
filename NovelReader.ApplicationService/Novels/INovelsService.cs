using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NovelReader.ApplicationService.Novels
{
    public interface INovelsService
    {
        Task<Models.Novel> ReadWords(int size, bool isDesc);
        Task<Models.Novel> ReadWordsPerBook(int size, bool isDesc);
    }
}
