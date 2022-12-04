namespace RabatLoty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;


            //Zmienne
            DateTime dateofBirth;
            DateTime dateOfFlight;
            string input;
            bool isInternational;
            bool isRegular;
            bool isSeasonal;
            bool isAhead;
            int rabat = 0;
            int rabatMax = 30;



            //Imię i nazwisko
            Console.Write("Wprowadź imię i nazwisko: ");
            var dane = Console.ReadLine().Split(" ");






        //Data urodzenia
        DateofBirth:
            Console.Write("Wprowadź datę urodzenia w odpowiednim formacie (RRRR.MM.DD): ");
            try
            {
                input = Console.ReadLine();

                if (input is null)
                {
                    Console.WriteLine("Proszę wpisać dane");
                    goto DateofBirth;
                }

                dateofBirth = new DateTime(int.Parse(input.Split(".")[0]), int.Parse(input.Split(".")[1]), int.Parse(input.Split(".")[2]));
            }
            catch
            {
                Console.WriteLine("Wprowadź poprawnie datę urodzenia.");
                goto DateofBirth;
            }
            if (dateofBirth > dateTime)
            {
                Console.WriteLine("Data urodzenia nie może być późniejsza niż obecna data");
                goto DateofBirth;
            }






        //Data Lotu
        DateofFlight:
            Console.Write("Data lotu (YYYY.MM.DD): ");
            try
            {
                input = Console.ReadLine();

                if (input is null)
                {
                    Console.WriteLine("Proszę wpisać poprawne dane");
                    goto DateofFlight;
                }

                dateOfFlight = new DateTime(int.Parse(input.Split('.')[0]), int.Parse(input.Split('.')[1]), int.Parse(input.Split('.')[2]));
            }
            catch
            {
                Console.WriteLine("Wprowadź poprawnie datę lotu.");
                goto DateofFlight;
            }
            if (dateOfFlight < dateTime)
            {
                Console.WriteLine("BŁĄD: Data lotu nie może być umiejscowiona w przeszłości");
                goto DateofFlight;
            }





        //Typ Lotu
        TypeofFlight:
            Console.Write("Czy zarezerwowany lot jest lotem międzynarodowym? T/N: ");
            input = Console.ReadLine();

            if (input is null)
            {
                Console.WriteLine("Proszę wprowadzić dane");
                goto TypeofFlight;
            }
            isInternational = false;
            if (input.ToLower().StartsWith("y") | input.ToLower().StartsWith("t"))
            {
                isInternational = true;
            }




        //Typ klienta
        TypeofPasager:
            isRegular = false;
            if (dateofBirth.AddYears(18) <= dateTime)
            {
                Console.Write("Czy pasażer jest stałym klientem (T/N): ");
                input = Console.ReadLine();

                if (input is null)
                {
                    Console.WriteLine("Proszę wpisać poprawne dane");
                    goto TypeofPasager;
                }

                if (input.ToLower().StartsWith("y") || input.ToLower().StartsWith("t"))
                    isRegular = true;
            }
            Console.WriteLine();



            //Sezonowość
            if (
                (dateOfFlight.Month == 7 || dateOfFlight.Month == 8) ||
                (dateOfFlight >= new DateTime(dateOfFlight.Year, 12, 20) || dateOfFlight <= new DateTime(dateOfFlight.Year, 1, 10)) ||
                (dateOfFlight >= new DateTime(dateOfFlight.Year, 3, 20)) && dateOfFlight <= new DateTime(dateOfFlight.Year, 4, 10))
            {
                isSeasonal = true;
            }
            else
                isSeasonal = false;
            Console.WriteLine($"Czy lot jest w sezonie: {(isSeasonal ? 't' : 'n')}");

            //wyprzedzenie
            if (dateTime.AddMonths(5) > dateOfFlight)
            {
                isAhead = false;
            }
            else
            {
                isAhead = true;
            }
            Console.WriteLine($"Czy lot jest zarezerwowany z wyprzedzeniem: {(isAhead ? 't' : 'n')}\n");





            //rozliczanie rabatów
            if (dateofBirth.AddYears(2) > dateTime)
            {
                Console.WriteLine("Niemowlę (rabat maksymalny 80%)");
                rabatMax = 80;
                if (isInternational)
                {
                    Console.WriteLine("Niemowlę, międzynarodowy lot(+70%)");
                    rabat += 70;

                }
                else
                {
                    Console.WriteLine("Niemowlę, lot krajowy (+80%)");
                    rabat += 80;
                }
            }
            else if (isInternational && isSeasonal)
            {
                Console.WriteLine("Międzynarodowy lot w sezonie (0%)");
                rabat = 0;

            }
            if (isInternational && !isSeasonal)
            {
                Console.WriteLine("Międzynarodowy lot poza sezonem(+15%)");
                rabat += 15;
            }
            if (dateofBirth.AddYears(2) <= dateTime && dateofBirth.AddMonths(16) > dateTime)
            {
                Console.WriteLine("Dziecko do lat 16 (+10%)");
                rabat += 10;
            }
            else if (isRegular)
            {
                Console.WriteLine("Stały klient (+15%)");
                rabat += 15;

            }
            if (dateTime.AddMonths(5) < dateOfFlight)
            {
                Console.WriteLine("Zakup z wyprzedzeniem (+10%)");
                rabat += 10;
            }

            //Górna granica rabatu 
            if (rabat > rabatMax)
            {
                Console.WriteLine($"Naliczony rabat {rabat}% przekracza limit rabat i zostanie zredukowany do {rabatMax}");
                rabat = rabatMax;
            }
            Console.WriteLine();
            Console.WriteLine($"Naliczony zostanie rabat w wysokości: {rabat}%");



        }
    }
}