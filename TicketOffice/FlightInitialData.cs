using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketOffice
{
    // Data structure for dictionary key in TicketOffice
    class FlightInitialData
    {
        public string departure;
        public string arrive;
        private DateTime date;

        public FlightInitialData(string departure, string arrive, string strDate)
        {
            this.departure = departure;
            this.arrive = arrive;
            date = DateTime.ParseExact(strDate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            if (obj is FlightInitialData other)
            {
                return departure == other.departure && arrive == other.arrive && date == other.date;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (departure != null ? departure.GetHashCode() : 0);
                hash = hash * 23 + (arrive != null ? arrive.GetHashCode() : 0);
                hash = hash * 23 + date.GetHashCode();
                return hash;
            }
        }
    }
}
