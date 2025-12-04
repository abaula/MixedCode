using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class MergedObjectBuilder : IMergedObjectBuilder
    {
        public IMergedObject BuildMergedObjectFromDiffs(List<IDiffObject> diffObjects)
        {
            var mergedObject = ModelFactory.CreateMergedObject();

            // формируем финальный список блоков со строками: оригинал + удалённые
            var finalBlocksList = _createMergedObjectBlocksForOriginalAndDeleted(diffObjects);

            // получаем блоки из добавленных строк
            var addBlocks = _getAddedBlocksFromDiffObjects(diffObjects);

            // добавляем блоки с добавленными строками в финальный список блоков
            int originalCurrentLineNumber = 0;
            int originalPreviousLineNumber = 0;
            var finalBlocksArray = finalBlocksList.ToArray();

            foreach (var block in finalBlocksArray)
            {
                if (block.State != DiffState.Original)
                    continue;

                originalCurrentLineNumber = block.OriginalTextTopBorderLineNumber;

                // перед каждым блоком оригинальных или удалённых строк получаем список блоков для вставки
                var blocksToInsertBefore = _findBlocks(addBlocks, originalCurrentLineNumber, originalPreviousLineNumber);

                // добавляем найденные блоки в результат
               _addBlocksToResultLinkedList(blocksToInsertBefore, finalBlocksList, block);

                originalPreviousLineNumber = originalCurrentLineNumber;
                originalCurrentLineNumber = block.OriginalTextBottomBorderLineNumber;
            }
            
            // добавляем строки добавленные в конец текста
            originalCurrentLineNumber++;

            // перед каждой оригинальной строкой текста получаем список блоков для вставки
            var blocksToInsertAfter = _findBlocks(addBlocks, originalCurrentLineNumber, originalPreviousLineNumber);
            
            // добавляем найденные блоки в результат
            _addBlocksToResultLinkedList(blocksToInsertAfter, finalBlocksList, null);

            // добавляем полученный список в MergedObject
            mergedObject.Blocks.AddRange(finalBlocksList);

            return mergedObject;
        }


        List<MergedObjectBlock> _findBlocks(List<MergedObjectBlock> source, int currentLine, int prevLine)
        {
            var found = source.Where(b =>
                                        b.OriginalTextBottomBorderLineNumber <= currentLine &&
                                        b.OriginalTextTopBorderLineNumber >= prevLine
                                    ).ToList();

            return found;
        }


        void _addBlocksToResultLinkedList(List<MergedObjectBlock> blocksToInsert, LinkedList<IMergedObjectBlock> insertTarget,  IMergedObjectBlock insertBefore)
        {
            // вставляем новые линии в  MergedObject
            foreach (var insertBlock in blocksToInsert)
            {
                if (insertBefore == null)
                {
                    insertTarget.AddLast(insertBlock);
                }
                else
                {
                    var node = insertTarget.Find(insertBefore);
                    insertTarget.AddBefore(node, insertBlock);                    
                }
            }
        }
        
        List<MergedObjectBlock> _getAddedBlocksFromDiffObjects(List<IDiffObject> diffObjects)
        {
            // создаём блоки для каждого Diff объекта, чтобы вычислить место вставки в оригинальный текст добавленных строк
            var blocks = new List<MergedObjectBlock>();

            foreach (var diffObject in diffObjects)
            {
                var diffObjectBlocks = _buildBlocksFromDiffObject(diffObject);
                blocks.AddRange(diffObjectBlocks);
            }

            // получаем блоки с добавленными строками
            var addBlocks = blocks.Where(b => b.State == DiffState.Added).ToList();

            return addBlocks;
        }
            
        LinkedList<IMergedObjectBlock> _createMergedObjectBlocksForOriginalAndDeleted(List<IDiffObject> diffObjects)
        {
            var result = new LinkedList<IMergedObjectBlock>();

            // Формируем общее множество строк из всех Diff объектов. 
            // Будем использовать для быстрого поиска.
            var allDiffLines = diffObjects.SelectMany(dfo => dfo.DiffLines).ToLookup(k => k.SourceTextLineNumber);

            // Формируем множество удалённых строк оригинального текста
            var deletedLineNumbers = diffObjects.SelectMany(b => b.DiffLines.Where(dl => dl.LineState == DiffState.Deleted).Select(l => l.SourceTextLineNumber)).Distinct().ToList();

            // Формируем множество оригинальных строк без изменений
            var originalTextLineNumbers = diffObjects.SelectMany(b => b.DiffLines.Where(dl => dl.LineState != DiffState.Added).Select(l => l.SourceTextLineNumber)).Distinct().ToList();

            // Обрабатываем все строки оригинального файла и формируем блоки
            var prevState = DiffState.Unknown;
            IMergedObjectBlock block = null;

            foreach (var lineNumber in originalTextLineNumbers.OrderBy(l => l))
            {
                // Получаем из всех источников, все варианты изменений строки.
                var diffList = allDiffLines[lineNumber];
                
                // Выбираем единственную строку из всех вариантов - оригинальную или удалённую.
                DiffState state = DiffState.Unknown;

                if (deletedLineNumbers.Any(n => n == lineNumber))
                    state = DiffState.Deleted;
                else
                    state = DiffState.Original;

                // здесь мы всегда без ошибки получаем лишь строки оригинального файла, т.к. состояние может принимать только 2 значения "Оригинальный" или "Удалённый"
                var dl = diffList.First(l => l.LineState == state);

                // если состояние очередной строки меняется, то создаём новый блок строк
                if (state != prevState)
                {
                    block = ModelFactory.CreateMergedObjectBlock();
                    block.State = state;
                    block.Source = dl.SourceText;
                    block.OriginalTextTopBorderLineNumber = lineNumber;

                    // сохраняем созданный блок в результате
                    result.AddLast(block);
                    
                    // запоминаем актуальное состояние
                    prevState = state;
                }

                block.OriginalTextBottomBorderLineNumber = lineNumber;
                block.DiffLines.Add(dl);
            }

            return result;
        }
            
        List<MergedObjectBlock> _buildBlocksFromDiffObject(IDiffObject diffObject)
        {
            var blocks = new List<MergedObjectBlock>();

            int originalTextLineNumber = 0;
            var state = DiffState.Unknown;
            MergedObjectBlock currentBlock = null;

            // 1. Создаём список блоков.
            // Каждый блок в списке содержит список добавленных строк.
            foreach (var diffLine in diffObject.DiffLines)
            {
                // Обновляем значение номера строки оригинального файла
                if (diffLine.LineState == DiffState.Original)
                {
                    originalTextLineNumber = diffLine.SourceTextLineNumber;
                }

                // Если строка меняет статус, значит начинается новый блок
                if (diffLine.LineState != state)
                {
                    // ... создаём новый блок
                    currentBlock = ModelFactory.CreateMergedObjectBlock();
                    
                    // ... обновляем значение верхней границы блока (номер строки в оригинальном файле)
                    currentBlock.OriginalTextTopBorderLineNumber = originalTextLineNumber;                    
                    
                    // ... обновляем значение источника текста для строки
                    currentBlock.Source = (diffLine.LineState == DiffState.Added)
                        ? diffObject.VersionText
                        : diffObject.OriginalText;

                    // ... задаём тип блока
                    currentBlock.State = diffLine.LineState;

                    // ... добавляем блок в список
                    blocks.Add(currentBlock);

                    // ... запоминаем состояние строки
                    state = diffLine.LineState;
                }

                // Добавляем линию в блок
                currentBlock.DiffLines.Add(diffLine);
            }

            // 2. Обновляем значение нижней границы у блоков
            int currentBorder = 0;

            // ... сначала обновляем границы у блоков с оригинальным текстом
            var originalBlocks = blocks.Where(b => b.State == DiffState.Original);

            foreach (var originalBlock in originalBlocks)
            {
                var topBorder = originalBlock.OriginalTextTopBorderLineNumber;
                var bottomBorder = topBorder + originalBlock.DiffLines.Count - 1;
                originalBlock.OriginalTextBottomBorderLineNumber = bottomBorder;

                // ... обновляем границы у остальных блоков, вставленных или удалённых
                var otherBlocks = blocks.Where(b => b.OriginalTextTopBorderLineNumber == currentBorder && b.State != DiffState.Original);

                foreach (var otherBlock in otherBlocks)
                {
                    otherBlock.OriginalTextBottomBorderLineNumber = topBorder;
                }

                currentBorder = bottomBorder;
            }

            // ... обновляем границы у блоков добавленных в конец текста
            var lastBlocks = blocks.Where(b => b.OriginalTextTopBorderLineNumber == currentBorder && b.State != DiffState.Original);
            
            foreach (var otherBlock in lastBlocks)
            {
                otherBlock.OriginalTextBottomBorderLineNumber = currentBorder + 1;
            }

            return blocks;
        }



    }
}
