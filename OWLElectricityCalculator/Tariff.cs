using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLElectricityCalculator
{
    public class Tariff
    {
        public List<Tuple<DateTime,DateTime>> Times { get; set; }
        public double CostPerkWh { get; set; }

        public bool WithinTariff(DateTime time)
        {
            return Times.Where(t => t.Item1 <= time && time <= t.Item2).Any();
        }
    }
}
