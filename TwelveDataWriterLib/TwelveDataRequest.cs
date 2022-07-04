using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace TwelveDataWriterLib
{
    public class TwelveDataRequest
    {
        public string url;

        public async Task<string> MakeRequest(string uri)
        {
            try
            {
                Console.WriteLine("Requesting data from 12Data");
                
                HttpClient httpClient = new HttpClient();
                string request = uri;
                HttpResponseMessage response =
                    (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                return responseBody;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Request failed: " + exception);
                return null;
            }
        }
    }
}
