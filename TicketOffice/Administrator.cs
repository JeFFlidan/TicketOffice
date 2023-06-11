using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TicketOffice
{
    // This class I used to fill json file with flights
    internal class Administrator
    {
        public static void WriteIntoJson()
        {
            string strDate = "12.06.2023 12:30";
            string strDate2 = "13.06.2023 10:15";
            List<string> ls = new List<string> { "Франкфурт" };
            Flight flight0 = new Flight(4567, "Київ", "Вашингтон", ls, strDate, strDate2, 200, new HashSet<Client>(), 30000);

            strDate = "12.06.2023 15:30";
            strDate2 = "13.06.2023 08:00";
            ls = new List<string> { "Париж" };
            Flight flight1 = new Flight(6141, "Київ", "Вашингтон", ls, strDate, strDate2, 250, new HashSet<Client>(), 39000);

            strDate = "13.06.2023 14:30";
            strDate2 = "13.06.2023 18:35";
            ls = new List<string> { "Варшава" };
            Flight flight2 = new Flight(7890, "Київ", "Берлін", ls, strDate, strDate2, 120, new HashSet<Client>(), 10000);


            strDate = "13.06.2023 08:45";
            strDate2 = "13.06.2023 12:35";
            ls = new List<string> { "Дніпро" };
            Flight flight3 = new Flight(8564, "Київ", "Львів", ls, strDate, strDate2, 80, new HashSet<Client>(), 3000);

            strDate = "13.06.2023 13:45";
            strDate2 = "13.06.2023 14:50";
            Flight flight4 = new Flight(9844, "Київ", "Львів", new List<string>(), strDate, strDate2, 70, new HashSet<Client>(), 4000);

            strDate = "15.06.2023 13:45";
            strDate2 = "15.06.2023 14:50";
            Flight flight5 = new Flight(1023, "Київ", "Дніпро", new List<string>(), strDate, strDate2, 70, new HashSet<Client>(), 3800);

            strDate = "15.06.2023 08:45";
            strDate2 = "15.06.2023 12:00";
            ls = new List<string> { "Харків" };
            Flight flight6 = new Flight(9879, "Київ", "Дніпро", ls, strDate, strDate2, 80, new HashSet<Client>(), 3300);


            strDate = "15.06.2023 17:45";
            strDate2 = "16.06.2023 18:35";
            ls = new List<string> { "Стамбул" };
            Flight flight7 = new Flight(6060, "Київ", "Токіо", ls, strDate, strDate2, 250, new HashSet<Client>(), 40000);

            strDate = "13.06.2023 17:30";
            strDate2 = "13.06.2023 19:35";
            Flight flight8 = new Flight(9632, "Київ", "Берлін", new List<string>(), strDate, strDate2, 100, new HashSet<Client>(), 14000);

            strDate = "15.06.2023 19:45";
            strDate2 = "16.06.2023 15:35";
            ls = new List<string> { "Дубай" };
            Flight flight9 = new Flight(8888, "Київ", "Токіо", ls, strDate, strDate2, 250, new HashSet<Client>(), 44000);

            strDate = "17.06.2023 16:45";
            strDate2 = "17.06.2023 20:00";
            Flight flight10 = new Flight(9999, "Харків", "Львів", new List<string>(), strDate, strDate2, 70, new HashSet<Client>(), 4500);

            strDate = "17.06.2023 08:45";
            strDate2 = "17.06.2023 13:30";
            ls = new List<string> { "Дніпро" };
            Flight flight11 = new Flight(7777, "Харків", "Львів", ls, strDate, strDate2, 80, new HashSet<Client>(), 3800);

            string flightStr0 = JsonConvert.SerializeObject(flight0);
            string flightStr1 = JsonConvert.SerializeObject(flight1);
            string flightStr2 = JsonConvert.SerializeObject(flight2);
            string flightStr3 = JsonConvert.SerializeObject(flight3);
            string flightStr4 = JsonConvert.SerializeObject(flight4);
            string flightStr5 = JsonConvert.SerializeObject(flight5);
            string flightStr6 = JsonConvert.SerializeObject(flight6);
            string flightStr7 = JsonConvert.SerializeObject(flight7);
            string flightStr8 = JsonConvert.SerializeObject(flight8);
            string flightStr9 = JsonConvert.SerializeObject(flight9);
            string flightStr10 = JsonConvert.SerializeObject(flight10);
            string flightStr11 = JsonConvert.SerializeObject(flight11);

            //JsonElement jsonElement = new JsonElement();

            JObject jsonObject = new JObject();
            jsonObject.Add("flight0", flightStr0);
            jsonObject.Add("flight1", flightStr1);
            jsonObject.Add("flight2", flightStr2);
            jsonObject.Add("flight3", flightStr3);
            jsonObject.Add("flight4", flightStr4);
            jsonObject.Add("flight5", flightStr5);
            jsonObject.Add("flight6", flightStr6);
            jsonObject.Add("flight7", flightStr7);
            jsonObject.Add("flight8", flightStr8);
            jsonObject.Add("flight9", flightStr9);
            jsonObject.Add("flight10", flightStr10);
            jsonObject.Add("flight11", flightStr11);

            //var flightsJson = JsonSerializer.Serialize(json, options);
            string filePath = "flights.json";
            File.WriteAllText(filePath, jsonObject.ToString());
        }
    }
}
