using System.Threading.Tasks;

namespace TwelveDataWriterLib
{
    public static class GoForData
    {
        public static async Task GetStocksData(string filename) {
            RequestComposer rc = new RequestComposer();
            string url = rc.ComposeUrl();
            TwelveDataRequest request = new TwelveDataRequest();
            string _response = await request.MakeRequest(url);
            CsvComposer.WriteToCSV(_response, filename);
            
            //Test url, let it be
            //string _response = await request.MakeRequest("https://api.twelvedata.com/time_series?symbol=AAPL,QQQ&apikey=cb2ddf16991d4fd1bff11908d5514dd3&interval=1min&outputsize=5&timezone=Europe/Moscow&06-28-2022%19:29:00&format=JSON");
        }
    }
}
