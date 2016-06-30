using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OWLElectricityCalculator
{
    public class DaySummary
    {
        public DateTime Date { get; set; }
        public double RunningCosts { get; set; }
        public double SolarSaved { get; set; }
        public double SolarExported { get; set; }
        public int MaxUsage { get; set; }
        public int MaxGenerating { get; set; }

        public static void PrintColumnNames(TextWriter writer)
        {
            writer.WriteLine("Date, RunningCosts, SolarSaved, SolarExported, MaxUsage, MaxGenerating");
        }

        public void PrintValues(TextWriter writer)
        {
            writer.WriteLine(string.Format("{0}, {1:N0}, {2:N0}, {3:N0}, {4}, {5}", Date.ToShortDateString(), RunningCosts, SolarSaved, SolarExported, MaxUsage, MaxGenerating));
        }
    }
}
