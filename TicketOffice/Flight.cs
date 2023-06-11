using System;
using System.Collections.Generic;

namespace TicketOffice
{
    internal class Flight
    {
        private string timeDiff;

        public int FlightNum { get; }
        public string DeparturePoint { get; }
        public string DestinationPoint { get; }
        public List<string> TransferPoints { get; }
        public string DepartureTime { get; }
        public string ArrivalTime { get; }
        public int FreeSeats { get; set; }
        public HashSet<Client> PersonWhoBooked { get; }
        public uint Price { get; }

        public Flight(
            int flightNum,
            string departurePoint,
            string destinationPoint,
            List<string> transferPoints,
            string departureTime,
            string arrivalTime,
            int freePlaces,
            HashSet<Client> personWhoBooked,
            uint price)
        {
            DeparturePoint = departurePoint;
            DestinationPoint = destinationPoint;
            TransferPoints = transferPoints;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            FreeSeats = freePlaces;
            FlightNum = flightNum;
            PersonWhoBooked = personWhoBooked;
            Price = price;

            DateTime departTime = DateTime.ParseExact(DepartureTime, "dd.MM.yyyy HH:m", System.Globalization.CultureInfo.InvariantCulture);
            DateTime arriveTime = DateTime.ParseExact(ArrivalTime, "dd.MM.yyyy HH:m", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan diff = arriveTime - departTime;
            timeDiff = diff.Hours.ToString() + ":" + diff.Minutes.ToString();
            timeDiff = Math.Floor(diff.TotalHours).ToString() + " год. " + diff.Minutes.ToString() + " хв.";
        }

        public bool BookSeat(Client newClient)
        {
            PersonWhoBooked.Add(newClient);
            --FreeSeats;
            return true;
        }

        public void CancelBooking(Client client)
        {
            PersonWhoBooked.Remove(client);
            ++FreeSeats;
        }

        public void PrintInfo()
        {
            Console.Write("{0}    \t", FlightNum);
            Console.Write("{0}    \t", DeparturePoint);
            Console.Write("{0}    \t", DestinationPoint);
            Console.Write("{0}\t", DepartureTime);
            Console.Write("{0}\t", ArrivalTime);
            Console.Write("{0}\t\t", timeDiff);
            if (TransferPoints.Count == 0)
            {
                Console.Write("-------\t\t");
            }
            else
            {
                foreach (string city in TransferPoints)
                {
                    Console.Write("{0} ", city);
                }
                Console.Write("    \t");
            }
            Console.Write("{0}    \t\t", FreeSeats);
            Console.WriteLine(Price);
        }
    }
}
