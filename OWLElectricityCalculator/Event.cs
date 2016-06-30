using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLElectricityCalculator
{
    public class Event
    {
        public DateTime Time { get; set; }
        int Property { get; set; }
        int SolarGenerated { get; set; }
        int SolarExport { get; set; }

        public int GridElectricityUsed { get; private set; }
        public int SolarElectricityUsed { get; private set; }
        public int SolarElectricityGenerated { get; private set; }
        public int SolarExported { get; private set; }

        public Event(DateTime time, int property, int solarGenerated, int solarExport)
        {
            Time = time;
            Property = property;
            SolarGenerated = solarGenerated;
            SolarExport = solarExport;

            SolarElectricityGenerated = SolarGenerated;
            SolarExported = SolarExport;
            GridElectricityUsed = Property;
            SolarElectricityUsed = SolarElectricityGenerated - GridElectricityUsed;
        }

        public Event(DateTime time, int property, int solarGenerated, int solarExport, Event previous)
        {
            Time = time;
            Property = property;
            SolarGenerated = solarGenerated;
            SolarExport = solarExport;

            SolarElectricityGenerated = SolarGenerated - previous.SolarGenerated;
            SolarExported = SolarExport - previous.SolarExport;
            GridElectricityUsed = Math.Max(Property - previous.Property - SolarElectricityGenerated, 0);
            SolarElectricityUsed = Property - previous.Property - GridElectricityUsed;
        }
    }
}
