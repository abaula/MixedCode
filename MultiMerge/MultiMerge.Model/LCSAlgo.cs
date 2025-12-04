using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class LCSAlgo
    {
        private Dictionary<long, List<int>> _solutions;

        public List<int> SequenceA { get; private set; }
        public List<int> SequenceB { get; private set; }

        public LCSAlgo(List<int> sequenceA, List<int> sequenceB)
        {
            SequenceA = sequenceA;
            SequenceB = sequenceB;
            _solutions = new Dictionary<long, List<int>>();
        }

        public List<int> BuildSequence()
        {
            return _lcsBack(SequenceA.Count, SequenceB.Count);
        }

        List<int> _lcsBack(int aSize, int bSize)
        {
            int aSubSize = (aSize - 1) < 0 ? 0 : (aSize - 1);
            int bSubSize = (bSize - 1) < 0 ? 0 : (bSize - 1);            
            long solutionKey = ((long)aSize << 32) + bSize;

            // если комбинация уже просчитывалась, то возвращаем результат из кэша
            if (_solutions.ContainsKey(solutionKey))
                return _solutions[solutionKey];

            var solution = new List<int>();

            if (aSize > 0 && bSize > 0)
            {
                if (SequenceA[aSize - 1] == SequenceB[bSize - 1])
                {
                    var n = _lcsBack(aSubSize, bSubSize);
                    solution.AddRange(n);
                    solution.Add(SequenceA[aSize - 1]);
                }
                else
                {
                    var x = _lcsBack(aSize, bSubSize);
                    var y = _lcsBack(aSubSize, bSize);
                    var maxLcs = (x.Count > y.Count) ? x : y;

                    solution.AddRange(maxLcs);
                }
            }

            // добавляем решение в кэш
            _solutions.Add(solutionKey, solution);

            return solution;
        }        
    }
}
