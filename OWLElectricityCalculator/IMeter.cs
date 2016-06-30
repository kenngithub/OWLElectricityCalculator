using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLElectricityCalculator
{
    public interface IMeter
    {
        double DailySupplyCharge();
        Tariff GetTariff(Event theEvent);
    }
}
