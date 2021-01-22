using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleParser
{
    public static class FileHelper
    {
        public static string ReadFile(string path)
        {
            try
            {
                if (VerifyFileExists(path))
                {
                    StreamReader sr = new StreamReader(path);
                    return System.IO.File.ReadAllText(path);
                }
                else {
                    throw new FileNotFoundException();
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }


        public static IEnumerable<WordCount> GetWordOccurences(string inputString)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool VerifyFileExists(string path)
        {
            return File.Exists(path);
        }

      
    }
}
