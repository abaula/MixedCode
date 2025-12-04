using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class UniqueTextLinesStorage : IUniqueTextLinesStorage
    {
        private Dictionary<string, int> _linesIndex;
        private Dictionary<int, string> _linesCodesIndex; 


        public UniqueTextLinesStorage()
        {
            _linesIndex = new Dictionary<string, int>();
            _linesCodesIndex = new Dictionary<int, string>();
        }

        public int AddLine(string text)
        {
            var trimmedText = text.Trim();

            if (_linesIndex.ContainsKey(trimmedText))
            {
                return _linesIndex[trimmedText];
            }

            int code = _linesIndex.Count + 1;

            // добавляем новую строку в оба индекса
            _linesIndex.Add(trimmedText, code);
            _linesCodesIndex.Add(code, trimmedText);

            return code;
        }

        public string GetLine(int code)
        {
            if (_linesCodesIndex.ContainsKey(code))
                return _linesCodesIndex[code];

            return null;
        }


       
    }
}
