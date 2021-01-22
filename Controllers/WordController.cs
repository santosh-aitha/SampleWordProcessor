using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SampleParser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {
        private readonly ILogger<WordController> _logger;

        public WordController(ILogger<WordController> logger)
        {
            _logger = logger;
        }

        const string path = @"..\SampleParser\localData\haiwatha.txt";
        [HttpGet]
        public IEnumerable<WordCount> Get()
        {
            var result = GetWordOccurences(ReadFile(path));
            return result.Select(wc => new WordCount { Word = wc.Word, Count = wc.Count }).OrderBy(x => x.Count).ToArray();

        }

        [Route("Top10Words")]
        [HttpGet]
        public IEnumerable<WordCount> GetTop10Occurences()
        {
            var result = GetWordOccurences(ReadFile(path));
            return result.Select(wc => new WordCount { Word = wc.Word, Count = wc.Count }).OrderByDescending(x => x.Count).ToArray().Take(10);

        }

        [Route("GetWordCount")]
        [HttpGet]
        public IEnumerable<WordCount> GetWordWithCount(string word)
        {
            var result = GetWordOccurences(ReadFile(path));
            return result.Where(x => x.Word.Equals(word)).Select(y => y); 
              
             
        }

        [Route("GetWordDefinition")]
        [HttpGet]
        public async Task<IEnumerable<WordCount>> GetWordWithCountAsync(string word)
        {
            var result = GetWordOccurences(ReadFile(path));
            WordCount resultWord = new WordCount();
            if (result.Count() > 0)
            {
                resultWord = result.Where(x => x.Word.Equals(word)).Select(y => y).FirstOrDefault();
                if (!string.IsNullOrEmpty(resultWord.Word))
                {
                    await GetOwlDefinitionsAsync(resultWord.Word);
                }

            }
            return result;
        }

        public async Task<string> GetOwlDefinitionsAsync(string word)
        {
            string endPoint = $"{Constants.OWL_ENDPOINT}{word}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.ACCESS_TOKEN);
            HttpResponseMessage response = await client.GetAsync(endPoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }
        public string ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            return System.IO.File.ReadAllText(path);
        }

        public IEnumerable<WordCount> GetWordOccurences(string inputString)
        {
            var result = inputString.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '\"' }, StringSplitOptions.RemoveEmptyEntries)
                 .GroupBy(r => r.Trim(), StringComparer.InvariantCultureIgnoreCase)
                 .Where(grp => grp.Count() >= 0)
                 .Select(grp => new WordCount
                 {
                     Word = grp.Key,
                     Count = grp.Count()

                 }).ToArray();
            return result;
        }

    }
}
