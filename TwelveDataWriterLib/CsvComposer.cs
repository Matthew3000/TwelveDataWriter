using System;
using System.IO;
using SimpleJSON;

namespace TwelveDataWriterLib
{
    public class CsvComposer
    {
        public static string CreateCSV()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //Creating new file with headers
            string filename = systemPath + "/Stonks_" + DateTime.Now.ToString("dd.MM.yyyy") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".csv";
            Console.WriteLine("File is saved to: " + filename);
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                //No "Name" of the stock, because API doesn't return it
                tw.WriteLine("Timestamp, Ticker, Value, Change");
            }
            return filename;
        }

        public static void WriteToCSV(string json, string filename)
        {
            JSONNode stockData = JSON.Parse(json);
            string[] tickers = Stonks.tickers;
            //if no error code for the request — proceeding with parsing
            if (!stockData["code"])
            {
                using (TextWriter tw = new StreamWriter(filename, true))
                {
                    //parse every ticker
                    for (int i = 0; i < tickers.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(tickers[i]))
                        {
                            //catching error codes for a ticker
                            if (!stockData[tickers[i]]["code"])
                            {
                                string dateTime = stockData[tickers[i]]["values"][0]["datetime"];
                                float value = stockData[tickers[i]]["values"][0]["close"];
                                float prevValue = stockData[tickers[i]]["values"][1]["close"];
                                float change = value - prevValue;
                                tw.WriteLine(dateTime + "," + tickers[i] + ",\"" + value + "\",\"" + change + "\"");
                                string text = "succesfuly written data for " + tickers[i] + " at " + dateTime;
                                Console.WriteLine(text);
                            }
                            //showing error code
                            else
                            {
                                string code = stockData[tickers[i]]["code"];
                                string error = stockData[tickers[i]]["message"];
                                string text = "no data for " + tickers[i] + " with error " + code + ": " + error;
                                Console.WriteLine(text);
                            }
                        }
                    }
                }
            }

            else
            {
                //showing error code
                string code = stockData["code"];
                string error = stockData["message"];
                string text = "failed with error code: " + code + " " + error;
                Console.WriteLine(text);
            }
        }
    }
}
