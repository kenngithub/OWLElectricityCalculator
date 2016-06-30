using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLElectricityCalculator
{
    public class SingleMeter : IMeter
    {
        Tariff Single;
        public double DailySupplyCharge() { return 78.1; }

        public SingleMeter()
        {
            Single = new Tariff()
            {
                Times = new List<Tuple<DateTime, DateTime>>() {
                    new Tuple<DateTime,DateTime>(new DateTime(2000, 1, 1, 00, 0, 0), new DateTime(2000, 1, 2, 00, 0, 0))
                },
                CostPerkWh = 27.39
            };
        }

        public Tariff GetTariff(Event theEvent)
        {
            return Single;
        }
    }
}
