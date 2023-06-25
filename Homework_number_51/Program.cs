using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_number_51
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandFindPerpetrators = "1";
            const string CommandShowAllPerpetrators = "2";
            const string CommandExit = "3";

            Database database = new Database();

            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                Console.WriteLine($"Для того что бы перейти к поиску преступников нажмите: {CommandFindPerpetrators}\n" +
                                  $"Для того что бы просмотреть список всех преступников нажмите: {CommandShowAllPerpetrators}\n" +
                                  $"Для того что бы закрыть приложение нажмите {CommandExit}\n");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandFindPerpetrators:
                        database.FindPerpetrators();
                        break;

                    case CommandShowAllPerpetrators:
                        database.ShowAllPerpetrators();
                        break;

                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет в наличии!");
                        break;
                }

                Console.WriteLine("\n\nДля продолжения ведите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Perpetrator
    {
        public Perpetrator(string surname, string name, string patronymic, string nationality, bool isUnderArrest, int growth, int weight)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            IsUnderArrest = isUnderArrest;
            Growth = growth;
            Weight = weight;
            Nationality = nationality;
        }

        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string Patronymic { get; private set; }
        public string Nationality { get; private set; }
        public bool IsUnderArrest { get; private set; }
        public int Growth { get; private set; }
        public int Weight { get; private set; }
    }

    class Database
    {
        private List<Perpetrator> _perpetrators = new List<Perpetrator>();

        public Database()
        {
            Fill();
        }

        public void FindPerpetrators()
        {
            int growth = GetNumber("Укажите рост преступника");
            int weight = GetNumber("Укажите вес преступника");
            bool isUnderArrest = false;

            Console.WriteLine("Укажите национальность преступника");
            string nationality = Console.ReadLine();

            Console.Clear();

            var filteredPerpetrators = _perpetrators.Where(Perpetrator => Perpetrator.Growth == growth &&
                                                                          Perpetrator.Weight == weight &&
                                                                          Perpetrator.Nationality == nationality &&
                                                                          Perpetrator.IsUnderArrest == isUnderArrest);

            foreach (Perpetrator perpetrator in filteredPerpetrators)
            {
                ShowPerpetrator(perpetrator);
            }
        }

        public void ShowAllPerpetrators()
        {
            for (int i = 0; i < _perpetrators.Count; i++)
            {
                ShowPerpetrator(_perpetrators[i]);
            }
        }

        private void ShowPerpetrator(Perpetrator perpetrator)
        {
            Console.WriteLine("Заключенные преступники:");
            Console.WriteLine($"ФИО: {perpetrator.Surname + " " + perpetrator.Name + " " + perpetrator.Patronymic}");
            Console.WriteLine($"Находится под арестом: {perpetrator.IsUnderArrest}");
            Console.WriteLine($"Рост: {perpetrator.Growth} см");
            Console.WriteLine($"Вес: {perpetrator.Weight} кг");
            Console.WriteLine($"Национальность: {perpetrator.Nationality}");
            Console.WriteLine();
        }

        private int GetNumber(string title)
        {
            bool isNumber = false;
            string userInput;
            int number = 0;

            while (isNumber == false)
            {
                Console.WriteLine(title);
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out number))
                {
                    isNumber = true;
                }
                else
                {
                    Console.WriteLine("Не верный формат вода");
                }
            }

            return number;
        }

        private void Fill()
        {
            Random random = new Random();

            List<string> surnames = new List<string>
           {
            "Иванов",
            "Петров",
            "Сидоров",
            "Смирнов",
            "Козлов",
            "Макаров",
            "Павлов",
            "Соколов",
            "Петрова",
            "Иванова"
           };
            List<string> names = new List<string>
           {
            "Иван",
            "Петр",
            "Алексей",
            "Ольга",
            "Андрей",
            "Максим",
            "Екатерина",
            "Игорь",
            "Анна",
            "Мария"
           };
            List<string> patronymics = new List<string>
            {
            "Иванович",
            "Петрович",
            "Васильевич",
            "Петровна",
            "Сергеевич",
            "Игоревич",
            "Александровна",
            "Сергеевич",
            "Владимировна",
            "Ивановна"
            };
            List<string> nationalities = new List<string>
            {
            "Русский",
            "Украинец",
            "Американец"
            };

            int quantityPerpetrators = 10;
            int minGrowth = 60;
            int maxGrowth = 170;
            int minWeight = 40;
            int maxWeight = 200;

            for (int i = 0; i < quantityPerpetrators; i++)
            {
                _perpetrators.Add(new Perpetrator(surnames[random.Next(surnames.Count)],
                                                  names[random.Next(names.Count)],
                                                  patronymics[random.Next(patronymics.Count)],
                                                  nationalities[random.Next(nationalities.Count)],
                                                  GetBool(random),
                                                  random.Next(minGrowth, maxGrowth),
                                                  random.Next(minWeight, maxWeight)));
            }
        }

        private bool GetBool(Random random)
        {
            double chanceFallingTrue = 0.50;

            return random.NextDouble() >= chanceFallingTrue;
        }
    }
}
