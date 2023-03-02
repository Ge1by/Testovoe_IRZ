namespace IRZ_Testovoe
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ввод количество кандидатов
            Console.WriteLine("Введите количество кандидатов:");
            string inputString = Console.ReadLine();
            int k;
            try
            {
                k = InputReader.ConvertStringToInt(inputString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Введен неправильный формат числа");
                Console.WriteLine("Нажмите любую кнопку, чтобы выключить программу");
                Console.ReadKey();
                return;
            }
            #endregion

            #region ввод данных о кандидатах
            List<Candidate> candidatesList = new List<Candidate>();
            Console.WriteLine("Ручной ввод данных: y/n");
            string answer = Console.ReadLine().ToLower();
            answer.Trim();
            switch (answer)
            {
                case "y":
                    candidatesList = InputReader.candidateFilling(k, candidatesList);
                    break;
                case "n":
                    candidatesList = InputReader.candidateAutoFilling(k, candidatesList);
                    break;
                default:
                    candidatesList = InputReader.candidateAutoFilling(k, candidatesList);
                    break;
            }
            #endregion

            #region вывод всех кандидатов
            for (int i = 0; i < candidatesList.Count; i++)
            {
                string candidateScores = string.Join(" ", candidatesList[i].results);
                Console.WriteLine("Номер кандидата: {0} Номер группы:{1} Результаты:{2} Сумма баллов:{3}", candidatesList[i].id, candidatesList[i].groupNumber, candidateScores, candidatesList[i].getResultsSum());
            }
            #endregion

            #region вывод результатов
            List<Candidate> firstTeam = getFirstTeam(candidatesList);
            Console.WriteLine("\nПервая команда:");
            for (int i = 0; i < firstTeam.Count; i++)
            {
                if(i != firstTeam.Count)
                {
                    Console.Write("{0} ", firstTeam[i].id);
                }
                else
                {
                    Console.WriteLine(firstTeam[i].id);
                }
                //string candidateScores = string.Join(" ", firstTeam[i].results);
                //Console.WriteLine("Номер группы:{0} Результаты:{1} Сумма баллов:{2}", firstTeam[i].groupNumber, candidateScores, firstTeam[i].getResultsSum());
            }

            int maxGroupNumber = candidatesList.Max(y => y.groupNumber); //вычисление наибольшего номера группы у введенных кандидатов
            List<Candidate> secondTeam = getSecondTeam(candidatesList, firstTeam, maxGroupNumber);
            Console.WriteLine("\nВторая команда:");
            for (int i = 0; i < secondTeam.Count; i++)
            {
                if (i != secondTeam.Count)
                {
                    Console.Write("{0} ", secondTeam[i].id);
                }
                else
                {
                    Console.WriteLine(secondTeam[i].id);
                }
                //string candidateScores = string.Join(" ", secondTeam[i].results);
                //Console.WriteLine("Номер группы:{0} Результаты:{1} Сумма баллов:{2}", secondTeam[i].groupNumber, candidateScores, secondTeam[i].getResultsSum());
            }
            #endregion

            #region ожидание закрытия программы
            Console.WriteLine("\nНажмите любую кнопку, чтобы выключить программу");
            Console.ReadKey();
            #endregion
        }

        //функция для определения первой команды
        public static List<Candidate> getFirstTeam(List<Candidate> candidates) 
        {
            List<Candidate> firstTeam = new List<Candidate>();
            
            for (int i = 0; i < 4;i++)
            {
                var bestCandidate = candidates.MaxBy(candidate => candidate.getResultsSum());
                firstTeam.Add(bestCandidate);
                candidates.Remove(bestCandidate);
            }

            firstTeam.Sort(delegate(Candidate x, Candidate y)
            {
                return x.id.CompareTo(y.id);
            });
            return firstTeam;
        }

        //функция для определения второй команды
        public static List<Candidate> getSecondTeam(List<Candidate> candidates, List<Candidate> firstTeam, int maxGroupNumber) 
        {
            List<Candidate> secondTeam = new List<Candidate>();

            foreach (Candidate candidate in firstTeam)
            {
                candidates.Remove(candidate);
            }

            while (secondTeam.Count < 4 )
            {
                var secondBestCandidate = candidates.MaxBy(candidate => candidate.getResultsSum());

                if(secondBestCandidate.groupNumber != maxGroupNumber)
                {
                    secondTeam.Add(secondBestCandidate);
                } 
                candidates.Remove(secondBestCandidate);
            }

            secondTeam.Sort(delegate (Candidate x, Candidate y)
            {
                return x.id.CompareTo(y.id);
            });

            return secondTeam;
        }
    }
}
