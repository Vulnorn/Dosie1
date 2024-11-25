using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;

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
                        AddDossier(fullNames,post);
                        break;

                    case MenuConsoleOutputDossier:
                        ShowAllDossiers(fullNames, post);
                        break;

                    case MenuConsoleDeliteDossier:
                        DeleteDossierByFullName(fullNames, post);
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

        static void AddDossier(string[] fullNames, string[] post)
        {
            string userCreateFullName;
            string userCreatePosition;

            Console.Clear();
            Console.WriteLine($"Введите ФИО Сотрудника:");
            userCreateFullName = Console.ReadLine().ToLower();

            if (FindElementArray(fullNames, userCreateFullName))
            {
                ReportAnError("Такой Сотрудник уже есть.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Введите должность для этого сотрудника:");
                userCreatePosition = Console.ReadLine().ToLower();

                ChangeElementArray(fullNames, userCreateFullName);
                ChangeElementArray(post, userCreatePosition);
            }
        }

        static void ShowAllDossiers(string[] fullNames, string[] post)
        {
            Console.Clear();

            if (fullNames.Length == 0)
            {
                ReportAnError("Досье пустое, заполните досье!");
            }
            else
            {
                for (int i = 0; i < fullNames.Length; i++)
                {
                    int numberOrder = i + 1;
                    Console.WriteLine($"{numberOrder}) {fullNames[i]} - {post[i]}");
                }

                Console.ReadKey();
            }
        }

        static void DeleteDossierByFullName(string[] fullNames, string[] post)
        {
            Console.WriteLine($"Введите ФИО сотрудника, которого нужно удалить из досье");
            string userInputWord = Console.ReadLine().ToLower();
            int index = 0;

            if (FindElementArray(fullNames, userInputWord, ref index))
            {
                ChangeElementArray(fullNames,index);
                ChangeElementArray(post, index);
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

        static bool FindElementArray(string[] array, string element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                    return true;
            }

            return false;
        }

        static bool FindElementArray(string[] array, string element, ref int index)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    index = i;
                    return true;
                }
            }

            return false;
        }

        static string[] ChangeElementArray(string[] array, string element)
        {
            string[] cacheArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                cacheArray[i] = array[i];
            }

            cacheArray[cacheArray.Length] = element;
            array = cacheArray;
            return array;
        }

        static string[] ChangeElementArray(string[] array, int index)
        {
            string[] cacheArray = new string[array.Length - 1];

            for (int i = index; i < array.Length - 1; i++)
            {
                string bufferElement = array[i];
                array[i] = array[i + 1];
                array[i + 1] = bufferElement;
            }

            for (int i = 0; i < cacheArray.Length; i++)
            {
                cacheArray[i] = array[i];
            }

            array = cacheArray;
            return array;
        }
    }
}