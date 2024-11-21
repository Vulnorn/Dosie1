using System;
using System.Reflection.Emit;

namespace Dossier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MenuConsoleAddDossier = 1;
            const int MenuConsoleOutputDossier = 2;
            const int MenuConsoleDeliteDossier = 3;
            const int MenuConsoleExit = 4;

            bool isWork = true;
            int userChoice;

            string[] fullNames = new string[0];
            string[] post = new string[0];  

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"\n{MenuConsoleAddDossier}. Добавить досье. " +
                    $"\n{MenuConsoleOutputDossier}. Вывести все досье." +
                    $"\n{MenuConsoleDeliteDossier}. Удалить досье." +
                    $"\n{MenuConsoleExit}. Выход.");
                userChoice = Convert.ToInt32(Console.ReadLine());

                switch (userChoice)
                {
                    case MenuConsoleAddDossier:
                        AddDossier(fullNames);
                        break;

                    case MenuConsoleOutputDossier:
                        ShowAllDossiers(fullNames);
                        break;

                    case MenuConsoleDeliteDossier:
                        DeleteDossierByFullName(fullNames);
                        break;

                    case MenuConsoleExit:
                        isWork = false;
                        break;

                    default:
                        ReportAnError("");
                        break;
                }
            }
        }

        static void AddDossier( string [] dossiers)
        {
            string userCreateFullName;
            string userCreatePosition;

            Console.Clear();
            Console.WriteLine($"Введите ФИО Сотрудника:");
            userCreateFullName = Console.ReadLine().ToLower();

            if (dossiers.ContainsKey(userCreateFullName))
            {
                ReportAnError("Такой Сотрудник уже есть.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Введите должность для этого сотрудника:");
                userCreatePosition = Console.ReadLine().ToLower();
                dossiers.Add(userCreateFullName, userCreatePosition);
            }
        }

        static void ShowAllDossiers(string[] dossiers)
        {
            Console.Clear();

            if (dossiers.Length == 0)
            {
                ReportAnError("Досье пустое, заполните досье!");
            }
            else
            {
                foreach (var item in dossiers)
                {
                    Console.Write($"{item} - {item};");
                }

                Console.ReadKey();
            }
        }

        static void DeleteDossierByFullName(string[] dossiers)
        {
            Console.WriteLine($"Введите ФИО сотрудника, которого нужно удалить из досье");
            string userInputWord = Console.ReadLine().ToLower();

            if (dossiers.ContainsKey(userInputWord))
            {
                dossiers.Remove(userInputWord);

                Console.WriteLine($"Досье удалено.");
                Console.ReadKey();
            }
            else
            {
                ReportAnError("Нет такого сотрудника");
            }

        }

        static void ReportAnError(string causeOfError)
        {
            Console.WriteLine($"Ошибка ввода. {causeOfError}");
            Console.ReadKey();
        }
    }
}