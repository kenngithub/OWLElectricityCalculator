using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLElectricityCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            const double exportPerWh = 8.0 / 1000;
            Event previousEvent = new Event(DateTime.MinValue, 0, 0, 0);
            IMeter meter = new SingleMeter();
            //IMeter meter = new TOUMeter();

            double runningCosts = 0.0;
            double solarSaved = 0.0;
            double solarExported = 0.0;
            DateTime currentDay = DateTime.MinValue;
            int maxUsage = 0;
            int maxGenerating = 0;
            List<DaySummary> summaries = new List<DaySummary>();

            string file = "2013-11";
            string inFilename = @"C:\Users\kenn\Downloads\solar\solar " + file + ".csv";
            //string outFilename = @"C:\Users\kenn\Downloads\solar\solar " + file + "-results.csv";
            string outFilename = @"C:\Users\kenn\Downloads\solar\solar " + file + "-results-Single.csv";

            using (StreamReader sr = new StreamReader(inFilename))
            {
                sr.ReadLine(); // skip column headings
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    DateTime time = DateTime.Parse(values[0]);
                    int currPropertyPowerUsage = Int32.Parse(values[1]);
                    int currSolarPowerGenerating = Int32.Parse(values[5]);

                    if (currentDay.Date != time.Date)
                    {
                        summaries.Add(new DaySummary()
                        {
                            Date = currentDay,
                            RunningCosts = runningCosts,
                            SolarSaved = solarSaved,
                            SolarExported = solarExported,
                            MaxUsage = maxUsage,
                            MaxGenerating = maxGenerating
                        });
                        previousEvent = new Event(DateTime.MinValue, 0, 0, 0);
                        runningCosts = 0.0;
                        solarSaved = 0.0;
                        solarExported = 0.0;
                        currentDay = time.Date;
                        maxUsage = 0;
                        maxGenerating = 0;
                    }

                    if (TimeZoneInfo.Local.IsDaylightSavingTime(time))
                    {
                        time = time.AddHours(1);
                    }
                    maxUsage = Math.Max(maxUsage, currPropertyPowerUsage);
                    maxGenerating = Math.Max(maxGenerating, currSolarPowerGenerating);

                    Event currentEvent = new Event(time, Int32.Parse(values[3]), Int32.Parse(values[7]), Int32.Parse(values[8]), previousEvent);
                    Tariff tarrif = meter.GetTariff(currentEvent);
                    double costPerWh = tarrif.CostPerkWh / 1000;

                    runningCosts += currentEvent.GridElectricityUsed * costPerWh;
                    solarSaved += currentEvent.SolarElectricityUsed * costPerWh;
                    solarExported += currentEvent.SolarExported * exportPerWh;

                    previousEvent = currentEvent;
                }
            }

            summaries.Add(new DaySummary()
            {
                Date = currentDay,
                RunningCosts = runningCosts,
                SolarSaved = solarSaved,
                SolarExported = solarExported,
                MaxUsage = maxUsage,
                MaxGenerating = maxGenerating
            });

            summaries.RemoveAt(0); // remove the blank line

            using (StreamWriter sw = new StreamWriter(outFilename, false))
            //using (StreamWriter sw = new StreamWriter(@"C:\Users\kenn.FRESHVIEW\Downloads\summaryTOU.csv", false))
            {
                DaySummary.PrintColumnNames(sw);
                foreach (DaySummary ds in summaries)
                {
                    ds.PrintValues(sw);
                }
            }
        }
    }
}
