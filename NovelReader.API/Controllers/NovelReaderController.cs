using Microsoft.AspNetCore.Mvc;
using NovelReader.API.Model;
using NovelReader.ApplicationService.Novels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NovelReader.API.Controllers
{
    public class NovelReaderController : Controller
    {
        private readonly INovelsService _novelsService;

        public NovelReaderController(INovelsService novelsService)
        {
            _novelsService = novelsService;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Index([FromQuery] PageRequest pageRequest)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var result = await _novelsService.ReadWords(pageRequest.Size, pageRequest.IsDesc);

            stopwatch.Stop();
            
            if (result == null)
            {
                return NotFound();
            }
            result.Duration = stopwatch.ElapsedMilliseconds;
            return Ok(result);
        }
    }
}
