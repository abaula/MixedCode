using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class DiffObjectBuilder : IDiffObjectBuilder
    {
        public IDiffObject BuildDiffObjectFromTexts(ITextObject originalText, ITextObject versionText)
        {
            // Находим наибольшую общую последовательность LCS
            var lcsAlgo = new LCSAlgo(originalText.Lines, versionText.Lines);
            var lcs = lcsAlgo.BuildSequence();

            // Создаём DiffObject
            var diffObject = ModelFactory.CreateDiffObject();
            diffObject.OriginalText = originalText;
            diffObject.VersionText = versionText;

            // Заполняем DiffObject значениями, по следующим правилам:
            // 1. Строки эталонного файла отсутствующие в LCS считаем удалёнными;
            // 2. Строки версионного файла отсутствующие в LCS считаем добавленными;
            // 3. Если для одной позиции существуют одновременно удалённые или добавленные строки, то группируем эти строки в блок изменений;
            // 4. Порядок строк в блоке изменений такой: удалённые, добавленные.

            int ethalonSequenceIndex = 0;
            int versionSequenceIndex = 0;
            int lcsSequenceIndex = 0;
            int currentLcsCode = 0;
            bool loop = true;

            while (loop)
            {
                if (lcsSequenceIndex < lcs.Count)
                    currentLcsCode = lcs[lcsSequenceIndex];

                // 1. Ищем удалённые строки
                var deletedLines = originalText.Lines.Skip(ethalonSequenceIndex).TakeWhile(l => l != currentLcsCode).ToList();

                if (deletedLines.Any())
                {                    
                    _addDiffObjectLines(diffObject, originalText, deletedLines, ethalonSequenceIndex + 1, DiffState.Deleted);
                    ethalonSequenceIndex += deletedLines.Count;
                }

                // 2. Ищем добавленные строки
                var addedLines = versionText.Lines.Skip(versionSequenceIndex).TakeWhile(l => l != currentLcsCode).ToList();

                if (addedLines.Any())
                {
                    _addDiffObjectLines(diffObject, versionText, addedLines, versionSequenceIndex + 1, DiffState.Added);
                    versionSequenceIndex += addedLines.Count;
                }

                // 3. Добавляем строки, которые не изменились
                if (lcsSequenceIndex < lcs.Count)
                {
                    var notChangedLines = new List<int>();
                    notChangedLines.Add(currentLcsCode);
                    _addDiffObjectLines(diffObject, originalText, notChangedLines, ethalonSequenceIndex + 1, DiffState.Original);
                }

                lcsSequenceIndex++;
                ethalonSequenceIndex++;
                versionSequenceIndex++;

                if (lcsSequenceIndex >= lcs.Count
                    && ethalonSequenceIndex >= originalText.Lines.Count
                    && versionSequenceIndex >= versionText.Lines.Count)
                    loop = false;

            }

            return diffObject;
        }




        void _addDiffObjectLines(IDiffObject diffObject, ITextObject sourceText, List<int> lines, int firstLineNumber, DiffState state)
        {
            foreach (var code in lines)
            {
                var diffLine = ModelFactory.CreateDiffObjectLine();
                diffLine.LineCode = code;
                diffLine.LineState = state;
                diffLine.SourceText = sourceText;
                diffLine.SourceTextLineNumber = firstLineNumber;

                diffObject.DiffLines.Add(diffLine);

                firstLineNumber++;
            }
        }


    }
}
