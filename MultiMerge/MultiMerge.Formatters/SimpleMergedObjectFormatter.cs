using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMerge.Model;

namespace MultiMerge.Formatters
{
    class SimpleMergedObjectFormatter : IMergedObjectFormatter
    {
        public StringBuilder GetFormattedText(IMergedObject mergedObject, IUniqueTextLinesStorage storage)
        {
            var sb = new StringBuilder();
            var addedBlocksList = new List<IMergedObjectBlock>();

            foreach (var block in mergedObject.Blocks)
            {
                if (block.State == DiffState.Added)
                {
                    // блоки с добавленным текстом набираем в кэш, чтобы обработать все такие блоки идущие подряд
                    addedBlocksList.Add(block);
                }
                else if (block.State == DiffState.Deleted)
                {
                    // обрабатываем накопленный буфер блоков с добавленными строками
                    _flushAddedBlocksBuffer(addedBlocksList, sb, storage);

                    // форматируем удалённые строки
                    _formatDeletedText(block, sb, storage);
                }
                else if (block.State == DiffState.Original)
                {
                    // обрабатываем накопленный буфер блоков с добавленными строками
                    _flushAddedBlocksBuffer(addedBlocksList, sb, storage);

                    // форматируем оригинальные строки
                    _formatOriginalText(block, sb, storage);
                }
                else
                {
                    // Что то пошло не так...
                    throw new ArgumentException(
                        "Как минимум один из блоков имеет статус, который не является ожидаемым в этой операции.");
                }
            }

            // обрабатываем накопленный буфер блоков с добавленными строками
            _flushAddedBlocksBuffer(addedBlocksList, sb, storage);

            return sb;
        }

        private void _flushAddedBlocksBuffer(List<IMergedObjectBlock> blocks, StringBuilder sb, IUniqueTextLinesStorage storage)
        {
            if (blocks.Any())
            {
                if (blocks.Count == 1)
                {
                    _formatAddedText(blocks.First(), sb, storage);                    
                }
                else
                {
                    // проверяем группу блоков - все ли блоки одинаковы?
                    var areEqual = _areAllBlocksAreEqual(blocks);

                    if (areEqual)
                    {
                        // форматируем любой блок добавленных строк
                        _formatAddedText(blocks.First(), sb, storage);
                    }
                    else
                    {
                        // форматируем конфликты
                        _formatAddedConflictText(blocks, sb, storage);
                    }
                }

                blocks.Clear();
            }
        }


        private bool _areAllBlocksAreEqual(List<IMergedObjectBlock> blocks)
        {
            Contract.Requires(blocks.Count > 1);

            var firstBlock = blocks.First();

            // сверяем все блоки с первым, начиная со второго
            for (int i = 1; i < blocks.Count; i++)
            {
                var block = blocks[i];

                // проверяем совпадение количества строк
                if (firstBlock.DiffLines.Count != block.DiffLines.Count)
                    return false;

                // проверяем совпадение каждой строки
                for (int j = 0; j < block.DiffLines.Count; j++)
                {
                    if (firstBlock.DiffLines[j].LineCode != block.DiffLines[j].LineCode)
                        return false;
                }
            }

            return true;
        }


        private void _formatAddedConflictText(List<IMergedObjectBlock> blocks, StringBuilder sb, IUniqueTextLinesStorage storage)
        {
            sb.AppendLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            sb.AppendLine("\tКонфликт вставки строк из нескольких файлов в одно место оригинального файла.");
            sb.AppendLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            // отображаем имена источников текста
            var sources = blocks.Select(b => b.Source.Source);
            var num = 1;

            foreach (var source in sources)
            {
                sb.AppendFormat("\t!@{0} ~ {1}", num++, Path.GetFileName(source));
                sb.AppendLine();
            }


            num = 1;
            foreach (var block in blocks)
            {
                var prefix = string.Format("!@{0}", num);
                _addFormattedLinesToStringBuilder(block.DiffLines, sb, prefix, storage);

                num++;
            }
        }

        private void _formatAddedText(IMergedObjectBlock block, StringBuilder sb, IUniqueTextLinesStorage storage)
        {
            _addFormattedLinesToStringBuilder(block.DiffLines, sb, "+", storage);
        }

        private void _formatOriginalText(IMergedObjectBlock block, StringBuilder sb, IUniqueTextLinesStorage storage)
        {
            _addFormattedLinesToStringBuilder(block.DiffLines, sb, string.Empty, storage);
        }

        private void _formatDeletedText(IMergedObjectBlock block, StringBuilder sb, IUniqueTextLinesStorage storage)
        {
            _addFormattedLinesToStringBuilder(block.DiffLines, sb, "-", storage);
        }

        private void _addFormattedLinesToStringBuilder(List<IDiffObjectLine> lines, StringBuilder sb, string prefix, IUniqueTextLinesStorage storage)
        {
            foreach (var line in lines)
            {
                sb.AppendFormat("{0}\t{1} {2}", line.SourceTextLineNumber, prefix, storage.GetLine(line.LineCode));
                sb.AppendLine();                
            }
        }

    }
}
