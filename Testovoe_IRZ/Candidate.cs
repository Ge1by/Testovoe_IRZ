using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRZ_Testovoe
{
    class Candidate
    {
        const int resultsCount = 5; //количество отборочных туров

        public int id; //номер кандидата 
        public int groupNumber; // номер группы кандидата
        public int[] results = new int[resultsCount]; //результаты отборочных туров
        public Candidate(int id, int groupNumber, int[] results) 
        {
            this.id = id;
            this.groupNumber = groupNumber;
            this.results = results;
         }

        public int getResultsSum()
        {
            return this.results.Sum();
        }
    }
}
