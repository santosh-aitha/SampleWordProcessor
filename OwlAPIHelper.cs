using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SampleParser
{
    public static class OwlAPIHelper
    {
        /// <summary>
        /// The method connects to the Owlbot api by providing the valid token and executes the request that has the word as input parameter
        /// The response contains the definition of the word 
        /// </summary>
        /// <param name="word"></param>
        /// <returns>definition of the word</returns>
        public static async Task<string> GetOwlDefinitionsAsync(string word)
        {
            try
            {
                string endPoint = $"{Constants.OWL_ENDPOINT}{word}";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.ACCESS_TOKEN);
                HttpResponseMessage response = await client.GetAsync(endPoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch 
            {
                throw;
            }

        }
    }
}
