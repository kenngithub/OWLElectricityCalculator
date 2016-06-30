using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLElectricityCalculator
{
    public class TOUMeter : IMeter
    {
        Tariff Peak;
        Tariff Offpeak;
        Tariff Shoulder;
        public double DailySupplyCharge() { return 87.175; }

        public TOUMeter()
        {
            Peak = new Tariff()
            {
                Times = new List<Tuple<DateTime, DateTime>>() {
                    new Tuple<DateTime,DateTime>(new DateTime(2000, 1, 1, 14, 0, 0), new DateTime(2000, 1, 1, 20, 0, 0))
                },
                CostPerkWh = 52.547
            };

            Offpeak = new Tariff()
            {
                Times = new List<Tuple<DateTime, DateTime>>() {
                    new Tuple<DateTime,DateTime>(new DateTime(2000, 1, 1, 0, 0, 0), new DateTime(2000, 1, 1, 7, 0, 0)),
                    new Tuple<DateTime,DateTime>(new DateTime(2000, 1, 1, 22, 0, 0), new DateTime(2000, 1, 2, 0, 0, 0))
                },
                CostPerkWh = 13.167
            };

            Shoulder = new Tariff()
            {
                Times = new List<Tuple<DateTime, DateTime>>() {
                    new Tuple<DateTime,DateTime>(new DateTime(2000, 1, 1, 7, 0, 0), new DateTime(2000, 1, 1, 14, 0, 0)),
                    new Tuple<DateTime,DateTime>(new DateTime(2000, 1, 1, 20, 0, 0), new DateTime(2000, 1, 1, 22, 0, 0))
                },
                CostPerkWh = 21.846
            };
        }

        public Tariff GetTariff(Event theEvent)
        {
            DateTime time = new DateTime(2000, 1, 1, theEvent.Time.Hour, theEvent.Time.Minute, theEvent.Time.Second);
            if (Offpeak.WithinTariff(time))
            {
                return Offpeak;
            }
            else if (Shoulder.WithinTariff(time))
            {
                return Shoulder;
            }
            else
            {
                if (theEvent.Time.DayOfWeek == DayOfWeek.Saturday || theEvent.Time.DayOfWeek == DayOfWeek.Sunday)
                {
                    return Shoulder;
                }
                else if (Peak.WithinTariff(time))
                {
                    return Peak;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Tariff", "There was no tariff that met this date, ");
                }
            }
        }
    }
}
