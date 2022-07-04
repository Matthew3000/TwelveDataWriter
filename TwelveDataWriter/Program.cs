using TwelveDataWriterLib;

namespace TwelveDataWriter
{
    class Program
    {
         public static void Main()
         {
            //creating new file
            string filename = CsvComposer.CreateCSV();

            //starting timer that fires requests for data
            MinuteTimer timer = new MinuteTimer(filename);
            timer.SetTimer();
         }
    }
}
