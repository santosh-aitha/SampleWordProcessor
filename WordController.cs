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
    public class WordController : ControllerBase
    {
        private readonly ILogger<WordController> _logger;

        public WordController(ILogger<WordController> logger)
        {
            _logger = logger;
        }

        const string path = @"localData\haiwatha.txt";
        [HttpGet("Word")]
        ///The api reference is used for parsing the entire text file and then find out the number of occurences of each of the words
        public IEnumerable<WordCount> Get()
        {
            var result = FileHelper.GetWordOccurences(FileHelper.ReadFile(path));
            return result.Select(wc => new WordCount { Word = wc.Word, Count = wc.Count }).OrderBy(x => x.Count).ToArray();

        }

        [HttpGet("Top10Words")]
        ///The api reference is used for parsing the entire text file and then find out the Top 10 occurences of words
        public IEnumerable<WordCount> GetTop10Occurences()
        {
            var result = FileHelper.GetWordOccurences(FileHelper.ReadFile(path));
            return result.Select(wc => new WordCount { Word = wc.Word, Count = wc.Count }).OrderByDescending(x => x.Count).ToArray().Take(10);

        }

       
        [HttpGet("GetWordCount")]
        ///The api reference is used for parsing the entire text file and then find out the word that is supplied as querystring param and displays occurences of word
        public IEnumerable<WordCount> GetWordWithCount(string word)
        {
            var result = FileHelper.GetWordOccurences(FileHelper.ReadFile(path));
            return result.Where(x => x.Word.Equals(word)).Select(y => y);


        }

       
        [HttpGet("GetWordDefinition")]
        ///The api reference is used contact OwlBot apis by passing a word as querystring parameter and gets back the definition of the word if found
        public async Task<IEnumerable<WordCount>> GetWordWithCountAsync(string word)
        {
            var result = FileHelper.GetWordOccurences(FileHelper.ReadFile(path));
            WordCount resultWord = new WordCount();
            if (result.Count() > 0)
            {
                resultWord = result.Where(x => x.Word.Equals(word)).Select(y => y).FirstOrDefault();
                if (!string.IsNullOrEmpty(resultWord.Word))
                {
                    await OwlAPIHelper.GetOwlDefinitionsAsync(resultWord.Word);
                }

            }
            return result;
        }






    }
}
