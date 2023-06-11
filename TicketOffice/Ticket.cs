using System;

namespace TicketOffice
{
    internal class Ticket
    {
        public Client Client { get; }
        public Flight Flight { get; }

        public int ID { get; }

        public Ticket(Client client, Flight flight, int id)
        {
            Client = client;
            Flight = flight;
            ID = id;
        }

        public void PrintInfo()
        {
            string flightDepAndDest = "Квиток " + Flight.DeparturePoint + " " + Flight.DestinationPoint;
            string textForLine = "=";
            string paddedText1 = textForLine.PadRight(15, '=');

            Console.WriteLine(paddedText1 + "   " + flightDepAndDest + "   " + paddedText1);
            Console.WriteLine("\nНомер рейсу: {0}", Flight.FlightNum);
            Console.WriteLine("Номер квитка: {0}", ID);
            Console.WriteLine("П.І.Б.: {0} {1} {2}", Client.Surname, Client.Name, Client.MiddleName);
            Console.WriteLine("Час відправлення: {0}", Flight.DepartureTime);
            Console.WriteLine("Час прибуття: {0}\n", Flight.ArrivalTime);

            string textForLine2 = "=";
            string paddedText2 = textForLine2.PadRight(36 + flightDepAndDest.Length, '=');
            Console.WriteLine(paddedText2);
        }
    }
}
