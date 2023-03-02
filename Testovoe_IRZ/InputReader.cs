using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRZ_Testovoe
{
    class InputReader
    {
        public static List<Candidate> candidateFilling(int k, List<Candidate> candidatesList) //ввод данных о кандидатах вручную
        {
            Console.WriteLine("Введите данные о кандидатах:");
            for (int i = 0; i < k; i++)
            {
                string text = Console.ReadLine();
                int[] results = new int[6];

                try
                {
                    results = Array.ConvertAll(text.Trim().Split(' '), ConvertStringToInt);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введен неправильный формат числа");
                    Console.WriteLine("Нажмите любую кнопку, чтобы выключить программу");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                
                Candidate candidate = new Candidate(i+1, results[0], results.Skip(1).ToArray());
                candidatesList.Add(candidate);
            }

            return candidatesList;
        }

        public static List<Candidate> candidateAutoFilling(int k, List<Candidate> candidatesList) //случайное генерирование данных о кандидатах
        {
            Random rnd = new Random();
            for (int i = 0; i < k; i++)
            {
                int groupNumber = rnd.Next(1,12);
                int[] results = new int[5];

                for (int j = 0; j < results.Length; j++)
                {
                    results[j] = rnd.Next(301);
                }

                Candidate candidate = new Candidate(i+1, groupNumber, results);
                candidatesList.Add(candidate);
            }

            return candidatesList;
        }
        public static int ConvertStringToInt(string input)
        {
            int number = int.Parse(input);
            return number;
        }
    }
}
