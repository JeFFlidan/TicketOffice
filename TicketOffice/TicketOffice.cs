using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TicketOffice
{
    internal class TicketOffice
    {
        private Dictionary<FlightInitialData, List<Flight>> allFlights;
        private Dictionary<int, Flight> chosenFlights;
        private Dictionary<int, Ticket> tickets;     // You can find ticket by ticket id

        private Random random;   // To generate ticket id

        public TicketOffice()
        {
            allFlights = new Dictionary<FlightInitialData, List<Flight>>();
            tickets = new Dictionary<int, Ticket>();
            random = new Random();

            string jsonContent = File.ReadAllText("flights.json");
            JObject json = JObject.Parse(jsonContent);

            foreach (JToken token in json.Values())
            {
                Flight tempFlight = JsonConvert.DeserializeObject<Flight>(token.ToString());
                FlightInitialData key = GetFlightInitialData(tempFlight);
                if (!allFlights.ContainsKey(key))
                {
                    allFlights[key] = new List<Flight>();
                }
                allFlights[key].Add(tempFlight);
            }
        }

        public void PrintAllFlights()
        {
            PrintHeaders();

            foreach (KeyValuePair<FlightInitialData, List<Flight>> pair in allFlights)
            {
                foreach (Flight flight in pair.Value)
                {
                    flight.PrintInfo();
                    Console.WriteLine();
                }
            }
        }

        public void PrintAllTickets()
        {
            Console.Clear();
            foreach (KeyValuePair<int, Ticket> pair in tickets)
            {
                pair.Value.PrintInfo();
                Console.WriteLine();
            }
        }

        public bool ChooseFlights(string departure, string destination, string date)
        {
            chosenFlights = new Dictionary<int, Flight>();
            FlightInitialData key = new FlightInitialData(departure, destination, date);
            if (!allFlights.ContainsKey(key))
            {
                return false;
            }

            PrintHeaders();
            List<Flight> flights = allFlights[key];
            foreach (Flight flight in flights)
            {
                if (flight.FreeSeats > 0)
                {
                    flight.PrintInfo();
                    chosenFlights[flight.FlightNum] = flight;
                }
            }

            return true;
        }

        public bool BookSeatForCertainFlight(int flightNum, string name, string secondName, string surname)
        {
            if (!CheckFlightNumber(flightNum))
                return false;

            Client client = new Client(name, secondName, surname);
            int id = random.Next(1, 20001);
            Ticket ticket = new Ticket(client, chosenFlights[flightNum], id);
            tickets[id] = ticket;
            return chosenFlights[flightNum].BookSeat(client);
        }

        public bool CancelBooking(int ticketId)
        {
            if (!tickets.ContainsKey(ticketId))
            {
                return false;
            }

            Ticket ticket = tickets[ticketId];
            ticket.Flight.CancelBooking(ticket.Client);
            tickets.Remove(ticketId);

            return true;
        }

        public void SaveInfoAboutFlights()
        {
            JObject jsonObject = new JObject();
            int counter = 0;
            foreach (KeyValuePair<FlightInitialData, List<Flight>> pair in allFlights)
            {
                foreach (Flight flight in pair.Value)
                {
                    string keyName = "flight" + counter.ToString();
                    string flightStr = JsonConvert.SerializeObject(flight);
                    jsonObject.Add(keyName, flightStr);
                    ++counter;
                }
            }

            string filePath = "flights.json";
            File.WriteAllText(filePath, jsonObject.ToString());
        }

        public bool CheckFlightNumber(int flightNum)
        {
            if (!chosenFlights.ContainsKey(flightNum))
            {
                return false;
            }

            return true;
        }

        public bool CheckIfClientBookedSeat(int flightNum, string name, string secondName, string surname)
        {
            Flight flight = chosenFlights[flightNum];
            HashSet<Client> clients = flight.PersonWhoBooked;
            Client client = new Client(name, secondName, surname);
            return clients.Contains(client);
        }

        public bool CheckIfEnoughFreeSeats(int flightNum, int seatsNumToBook)
        {
            Flight flight = chosenFlights[flightNum];
            if (flight.FreeSeats - seatsNumToBook < 0)
            {
                return false;
            }
            return true;
        }



        private FlightInitialData GetFlightInitialData(Flight flight)
        {
            string date = flight.DepartureTime;
            int indexOfSpace = date.IndexOf(' ');
            string result = date.Substring(0, indexOfSpace);

            FlightInitialData flightInitial = new FlightInitialData(flight.DeparturePoint, flight.DestinationPoint, result);
            return flightInitial;
        }

        private void PrintHeaders()
        {
            Console.Write("Номер    \t");
            Console.Write("Пункт    \t");
            Console.Write("Пункт    \t");
            Console.Write("Час           \t\t");
            Console.Write("Час           \t\t");
            Console.Write("Тривалість    \t\t");
            Console.Write("Пересадки     \t");
            Console.Write("Вільні    \t");
            Console.Write("Вартість");
            Console.WriteLine();
            Console.Write("рейсу    \t");
            Console.Write("відправлення\t");
            Console.Write("прибуття    \t");
            Console.Write("відправлення    \t");
            Console.Write("прибуття    \t\t");
            Console.Write("рейсу       \t\t\t\t");
            Console.Write("місця     \t");
            Console.Write("квитка");
            Console.WriteLine();
            string text = "=";
            string paddedText = text.PadRight(160, '=');
            Console.WriteLine(paddedText);
        }
    }
}
