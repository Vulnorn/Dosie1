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
            const string ComandAddDossier = "1";
            const string ComandOutputDossier = "2";
            const string ComandDeliteDossier = "3";
            const string ComandOutputLastNames = "4";
            const string ComandExit = "5";

            bool isWork = true;

            string[] fullNames = new string[0];
            string[] post = new string[0];

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"\n{ComandAddDossier}. Добавить досье. " +
                    $"\n{ComandOutputDossier}. Вывести все досье." +
                    $"\n{ComandDeliteDossier}. Удалить досье." +
                    $"\n{ComandOutputLastNames}. Показать всех сотрудников на фамили -" +
                    $"\n{ComandExit}. Выход.");
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case ComandAddDossier:
                        AddDossier(ref fullNames, ref post);
                        break;

                    case ComandOutputDossier:
                        ShowAllDossiers(fullNames, post);
                        break;

                    case ComandDeliteDossier:
                        DeleteDossierByFullName(ref fullNames, ref post);
                        break;

                    case ComandExit:
                        isWork = false;
                        break;

                    case ComandOutputLastNames:
                        ShowAllLastNames(fullNames, post);
                        break;

                    default:
                        ReportAnError("");
                        break;


                }
            }
        }

        static void AddDossier(ref string[] fullNames, ref string[] post)
        {
            string userCreateFullName;
            string userCreatePosition;

            Console.Clear();
            Console.WriteLine($"Введите ФИО Сотрудника:");
            userCreateFullName = Console.ReadLine().ToLower();

            Console.WriteLine($"Введите должность для этого сотрудника:");
            userCreatePosition = Console.ReadLine().ToLower();
            fullNames = EditElementArray(fullNames, userCreateFullName);
            post = EditElementArray(post, userCreatePosition);
        }

        static void ShowAllDossiers(string[] fullNames, string[] post)
        {
            Console.Clear();

            if (CheckingAbsenceEmptiness(fullNames))
            {
                for (int i = 0; i < fullNames.Length; i++)
                {
                    int numberOrder = i + 1;
                    Console.WriteLine($"{numberOrder}) {fullNames[i]} - {post[i]}");
                }

                Console.ReadKey();
            }
        }

        static void ShowAllLastNames(string[] fullNames, string[] post)
        {
            if (CheckingAbsenceEmptiness(fullNames))
            {
                Console.Clear();
                Console.WriteLine($"Введите Фамилию");
                string userInputWord = Console.ReadLine().ToLower();
                int showLastName = 0;

                for (int i = 0; i < fullNames.Length; i++)
                {
                    char symbolSeparation = ' ';
                    string withFullName = fullNames[i];
                    string[] words = withFullName.Split(symbolSeparation);
                    string withLastName = words[0];

                    if (userInputWord == withLastName)
                    {
                        Console.WriteLine($"{fullNames[i]}");
                        showLastName++;
                    }
                }

                if (showLastName == 0)
                    ReportAnError("Нет таких фамилий");
            }
        }

        static void DeleteDossierByFullName(ref string[] fullNames, ref string[] post)
        {
            if (CheckingAbsenceEmptiness(fullNames))
            {
                Console.WriteLine($"Введите ФИО сотрудника, которого нужно удалить из досье");
                string userInputWord = Console.ReadLine().ToLower();
                int index = 0;

                if (FindElementArray(fullNames, userInputWord, ref index))
                {
                    fullNames = EditElementArray(fullNames, index);
                    post = EditElementArray(post, index);
                }
                else
                {
                    ReportAnError("Нет такого сотрудника");
                }
            }
        }

        static bool CheckingAbsenceEmptiness(string[] fullNames)
        {
            if (fullNames.Length == 0)
            {
                ReportAnError("Досье пустое, заполните досье!");
                return false;
            }
            else
            {
                return true;
            }
        }

        static void ReportAnError(string causeOfError)
        {
            Console.WriteLine($"Ошибка ввода. {causeOfError}");
            Console.ReadKey();
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

        static string[] EditElementArray(string[] array, string element)
        {
            string[] cacheArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                cacheArray[i] = array[i];
            }

            cacheArray[cacheArray.Length - 1] = element;
            array = cacheArray;

            return array;
        }

        static string[] EditElementArray(string[] array, int index)
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