using System;
using System.Threading.Tasks;
using System.Timers;

namespace TwelveDataWriterLib
{
    public class MinuteTimer
    {
        private static Timer aTimer;
        public string filename;
        public MinuteTimer(string n) { filename = n; }

        public void SetTimer()
        {
            //First fire before the timers starts
            Task.Run(() => GoForData.GetStocksData(filename));
            StartTimer();
        }

        public void StartTimer()
        {
            // Create a timer with a one minute interval.
            aTimer = new Timer(60000);
            //Starting the request sequense
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            //waiting for user to end the program
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            await GoForData.GetStocksData(filename);
        }
    }
}
