using System;
using System.Collections.Generic;
using System.Linq;

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

            Dictionary<string, string> dossiers = new Dictionary<string, string>();

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"\n{MenuConsoleAddDossier}. Добавить досье. \n\n{MenuConsoleOutputDossier}. Вывести все досье.\n\n {MenuConsoleDeliteDossier}. Удалить досье.\n\n{MenuConsoleExit}. Выход.");
                userChoice = Convert.ToInt32(Console.ReadLine());

                switch (userChoice)
                {
                    case MenuConsoleAddDossier:
                        AddDossier(dossiers);
                        break;

                    case MenuConsoleOutputDossier:
                        ShowAllDossiers(dossiers);
                        break;

                    case MenuConsoleDeliteDossier:
                        DeleteDossierByFullName(dossiers);
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

        static void AddDossier(Dictionary<string, string> dossiers)
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

        static void ShowAllDossiers(Dictionary<string, string> dossiers)
        {
            Console.Clear();

            if (dossiers.LongCount() == 0)
            {
                ReportAnError("Досье пустое, заполните досье!");
            }
            else
            {
                foreach (var item in dossiers)
                {
                    Console.Write($"{item.Key} - {item.Value};");
                }

                Console.ReadKey();
            }
        }

        static void DeleteDossierByFullName(Dictionary<string, string> dossiers)
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