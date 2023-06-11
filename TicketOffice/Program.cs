using System;
using System.Text;

namespace TicketOffice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("Cyrillic");
            TicketOffice ticketOffice = new TicketOffice();

            while (true)
            {
                Console.WriteLine("1 = Вивести всі рейси");
                Console.WriteLine("2 = Почати бронювання");
                Console.WriteLine("3 = Відмінити бронювання");
                Console.WriteLine("4 = Роздрукувати квітки");
                Console.WriteLine("5 = Оновити сторінку");
                Console.WriteLine("6 = Вихід та роздрукування квитків");

                Console.Write("Оберіть дію: ");
                int action;
                try
                {
                    action = int.Parse(Console.ReadLine());
                }
                catch(FormatException)
                {
                    PrintError("Будь ласка, введіть число\n");
                    continue;
                }

                Console.WriteLine("\n");

                if (action < 1 || action > 6)
                {
                    string error = string.Format("Число {0} не відповідає ніякій дії. Будь ласка, спробуйте ще раз.", action);
                    PrintError(error);
                    continue;
                }
                else if (action == 1)
                {
                    string stringForPadding = "=";
                    string paddedString = stringForPadding.PadRight(68, '=');
                    PrintSuccess(paddedString + "   Перелік усіх рейсів   " + paddedString);
                    Console.WriteLine();
                    ticketOffice.PrintAllFlights();
                    Console.WriteLine();
                    continue;
                }
                else if (action == 3)
                {
                    CancelBooking(ticketOffice);
                    continue;
                }
                else if (action == 4)
                {
                    ticketOffice.PrintAllTickets();
                    continue;
                }
                else if (action == 5)
                {
                    Console.Clear();
                    PrintSuccess("Ви оновили сторінку!\n\n");
                    continue;
                }
                else if (action == 6)
                {
                    ticketOffice.PrintAllTickets();
                    break;
                }

                PrintSuccess("Ви розпочали бронювання квитків!");

                string departurePoint = GetDeparturePoint();
                string arrivalPoint = GetArrivalPoint();
                string date = GetDate();

                if (!ticketOffice.ChooseFlights(departurePoint, arrivalPoint, date))
                {
                    string error = string.Format("Рейсів {0} - {1} не існує у {2}\n", departurePoint, arrivalPoint, date);
                    PrintError(error);
                    continue;
                }

                int flightNum = GetFlightNum(ticketOffice);
                if (flightNum == -1)
                {
                    Console.Clear();
                    PrintSuccess("Ви повернулися у меню.\n");
                    continue;
                }

                int seatsAmount = GetSeatsAmount(ticketOffice, flightNum);
                if (seatsAmount == -1)
                {
                    Console.Clear();
                    PrintSuccess("Ви повернулися у меню.\n");
                    continue;
                }

                if (!BookSeats(ticketOffice, flightNum, seatsAmount))
                {
                    Console.Clear();
                    PrintSuccess("Ви повернулися у меню.\n");
                    continue;
                }

                if (seatsAmount == 1)
                {
                    PrintSuccess("Ви успішно забронювали 1 квиток!\n");
                }
                else if (seatsAmount < 5)
                {
                    string msg = string.Format("Ви успішно забронювали {0} квитки!\n", seatsAmount);
                    PrintSuccess(msg);
                }
                else
                {
                    string msg = string.Format("Ви успішно забронювали {0} квитків!\n", seatsAmount);
                    PrintSuccess(msg);
                }
            }

            ticketOffice.SaveInfoAboutFlights();
        }


        static void CancelBooking(TicketOffice ticketOffice)
        {
            ticketOffice.PrintAllTickets();
            PrintSuccess("\nВи можете відмінити бронювання\n");
            while (true)
            {
                Console.Write("Будь ласка, введіть номер квитка або -1 для повернення у меню: ");
                try
                {
                    int idOrAction = int.Parse(Console.ReadLine());
                    if (idOrAction == -1)
                    {
                        Console.Clear();
                        break;
                    }
                    if (!ticketOffice.CancelBooking(idOrAction))
                    {
                        string error = string.Format("\nНе існує квитка з номером {0}. Спробуйте ще раз\n", idOrAction);
                        PrintError(error);
                    }
                    else
                    {
                        string msg = string.Format("Ви успішно відмінили бронювання квитка з номером {0}\n", idOrAction);
                        PrintSuccess(msg);
                    }
                }
                catch (FormatException)
                {
                    PrintError("\nБудь ласка, введіть число\n");
                }

                continue;
            }
        }

        static string GetDeparturePoint()
        {
            string departurePoint;
            while (true)
            {
                Console.Write("Місце відправлення: ");
                departurePoint = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departurePoint))
                {
                    PrintError("\nВведіть коректне місце відправлення!!!\n");
                    continue;
                }
                break;
            }

            return departurePoint;
        }

        static string GetArrivalPoint()
        {
            string arrivalPoint;
            while (true)
            {
                Console.Write("Місце призначення: ");
                arrivalPoint = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(arrivalPoint))
                {
                    PrintError("\nВведіть коректний пунк призначення!!!");
                    continue;
                }
                break;
            }
            return arrivalPoint;
        }

        static bool BookSeats(TicketOffice ticketOffice, int flightNum, int seatsAmount)
        {
            int counter = 0;
            string name;
            string secondName;
            string surname;
            while (counter != seatsAmount)
            {
                PrintError("\nУВАГА! При вводі особистих даних перевірте йх на відповідність документу, який підтверджує особистість");
                while (true)
                {
                    Console.Write("Введіть прізвище: ");
                    surname = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(surname))
                    {
                        PrintError("\nВведіть коректне прізвище!!!");
                        continue;
                    }

                    break;
                }

                while (true)
                {
                    Console.Write("Введіть ім'я: ");
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        PrintError("\nВведіть коректне ім'я!!!\n");
                        continue;
                    }

                    break;
                }

                while (true)
                {
                    Console.Write("Введіть ім'я по батькові: ");
                    secondName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(secondName))
                    {
                        PrintError("\nВведіть коректне ім'я по батькові!!!\n");
                        continue;
                    }

                    break;
                }

                int action = -10;
                while (true)
                {
                    Console.WriteLine("\nЧи впевнені ви в коректності даних?\n0 = Hi\n1 = Так\n-1 = Відмінити бронювання та повернутися у меню");
                    Console.Write("Ваш вибір: ");
                    try
                    {
                        action = int.Parse(Console.ReadLine());
                        if (action != -1 && action != 0 && action != 1)
                        {
                            PrintError("\nБудь ласка, введіть 0, 1 або -1.");
                            continue;
                        }
                    }
                    catch (FormatException)
                    {
                        PrintError("\nБудь ласка, введіть число.\n");
                    }

                    break;
                }

                if (action == -1)
                {
                    return false;
                }

                if (action == 0)
                {
                    PrintError("\nБудь ласка, введіть нові дані");
                    continue;
                }

                if (!ticketOffice.CheckIfClientBookedSeat(flightNum, name, secondName, surname))
                {
                    ticketOffice.BookSeatForCertainFlight(flightNum, name, secondName, surname);
                    string msg = string.Format("\nВи успіншо забронювали квиток для {0} {1} {2}\n", surname, name, secondName);
                    PrintSuccess(msg);
                    ++counter;
                }
                else
                {
                    PrintError("\nМісце для цієї людини вже заброньовано! Спробуйте ще раз");
                }
            }

            return true;
        }

        static int GetSeatsAmount(TicketOffice ticketOffice, int flightNum)
        {
            int seatsAmount = 0;
            while (true)
            {
                Console.Write("Введіть кількість місць або -1 для повернення у меню: : ");
                try
                {
                    seatsAmount = int.Parse(Console.ReadLine());
                    if (seatsAmount == -1)
                    {
                        return seatsAmount;
                    }

                    if (seatsAmount <= 0)
                    {
                        PrintError("\nМінімальна кількість місць для броюванна - 1. Спробуйте ще раз.\n");
                        continue;
                    }
                    if (!ticketOffice.CheckIfEnoughFreeSeats(flightNum, seatsAmount))
                    {
                        string error = string.Format("\nНе можна забронювати {0} місць! Спробуйте ще раз.", seatsAmount);
                        PrintError(error);
                        continue;
                    }
                }
                catch (FormatException)
                {
                    PrintError("\nБудь ласка, введіть число!!!\n");
                    continue;
                }
                break;
            }

            return seatsAmount;
        }

        static int GetFlightNum(TicketOffice ticketOffice)
        {
            int flightNum;
            while (true)
            {
                Console.Write("\nОберіть номер рейсу з попереднього списку або введіть -1 для повернення у меню: ");

                try
                {
                    flightNum = int.Parse(Console.ReadLine());
                    if (flightNum == -1)
                    {
                        return flightNum;
                    }
                }
                catch (FormatException)
                {
                    PrintError("\nБудь ласка, введіть число!!!\n");
                    continue;
                }

                if (ticketOffice.CheckFlightNumber(flightNum))
                {
                    return flightNum;
                }
                else
                {
                    string error = string.Format("Не існує рейсу з номером {0}. Спробуйте ще раз.", flightNum);
                    PrintError(error);
                }
            }
        }

        static string GetDate()
        {
            string date;
            while (true)
            {
                Console.Write("Дата відправлення (у форматі дд.мм.рррр): ");
                date = Console.ReadLine();
                try
                {
                    DateTime tempDate = DateTime.ParseExact(date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    PrintError("\nБудь ласка, введіть дату правильного формату дд.мм.рррр!!!\n");
                    continue;
                }

                break;      // If everything is correct, this loop is broken and you can work with the main programme
            }

            Console.WriteLine();

            return date;
        }

        static void PrintError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
