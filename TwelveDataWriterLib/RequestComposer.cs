using System;

namespace TwelveDataWriterLib
{
    public class RequestComposer
    {
        private const string twelveDataUrl = "https://api.twelvedata.com/time_series?";
        //hardcoding all the attributes because not specified otherwise in a task
        private const string apiKey = "apikey=cb2ddf16991d4fd1bff11908d5514dd3";
        private string interval = "interval=1min";
        private string outputSize = "outputsize=2";
        private string format = "format=JSON";
        private string timezone = "timezone=Europe/Moscow";
        private string startDate;
        private string[] tickers = Stonks.tickers;
        private string tickersTosend = "symbol=";
        private string request;

        public string ComposeUrl()
        {
            //ComposeUrl method composes the request url
            //clearing previous request. The request is always made as a whole from scratch in order
            //to make possible following modication such as customizable in runtime attributes
            request = "";

            //start date for the request
            DateTime requestTime = DateTime.Now;
            startDate = "start_date=" + requestTime.ToString("yyyy-MM-dd HH") + ":" + (Convert.ToInt32(requestTime.ToString("mm")) - 3) + ":00";

            //concat tickers. Again starting from scratch
            tickersTosend = "symbol=";
            for (int i = 0; i < tickers.Length; i++)
            {
                if (i != 0 && !string.IsNullOrWhiteSpace(tickers[i]))
                    tickersTosend += ",";
                if (!string.IsNullOrWhiteSpace(tickers[i]))
                    tickersTosend += tickers[i];
            }

            request = twelveDataUrl + tickersTosend + "&" + apiKey + "&" + interval + "&" + outputSize + "&" + timezone + "&" + startDate + "&" + tickersTosend + "&" + format;
            return request;
        }
    }
}
