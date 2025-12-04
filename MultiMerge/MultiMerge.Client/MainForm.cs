using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MultiMerge.Formatters;
using MultiMerge.Model;
using MultiMerge.Utils;

namespace MultiMerge.Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEthalonFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                txtEthalonFile.Text = openFileDialog.FileName;
            }
        }

        private void btnVersion1File_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                txtVersion1File.Text = openFileDialog.FileName;
            }

        }

        private void btnVersion2File_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                txtVersion2File.Text = openFileDialog.FileName;
            }

        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            // Делаем проверку, что все 3 пути к файлам указаны

            if (string.IsNullOrWhiteSpace(txtEthalonFile.Text))
            {
                _showOperationAbortMessageBox("Не указан эталонный файл.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtVersion1File.Text))
            {
                _showOperationAbortMessageBox("Не указан файл 1-й версии.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtVersion2File.Text))
            {
                _showOperationAbortMessageBox("Не указан файл 2-й версии.");
                return;
            }

            // Выполняем слияние
            _merge();
        }


        private void _showOperationAbortMessageBox(string message)
        {
            MessageBox.Show(message, @"Операция отменена.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _merge()
        {
            // 1. Получаем результат в виде объекта
            var storage = ModelFactory.CreateUniqueFileLinesStorage();
            var originalFile = _createTextObjectFromFile(txtEthalonFile.Text, storage);
            var version1File = _createTextObjectFromFile(txtVersion1File.Text, storage);
            var version2File = _createTextObjectFromFile(txtVersion2File.Text, storage);

            var diffObjectBuilder = ModelFactory.CreateDiffObjectBuilder();
            var diffObjectsList = new List<IDiffObject>();

            var diffObject1 = diffObjectBuilder.BuildDiffObjectFromTexts(originalFile, version1File);
            diffObjectsList.Add(diffObject1);

            var diffObject2 = diffObjectBuilder.BuildDiffObjectFromTexts(originalFile, version2File);
            diffObjectsList.Add(diffObject2);

            var mergedObjectBuilder = ModelFactory.CreateMergedObjectBuilder();
            var mergedObject = mergedObjectBuilder.BuildMergedObjectFromDiffs(diffObjectsList);

            // 2. Форматируем результат в текст
            var simpleFormatter = FormattersFactory.CreateMergedObjectFormatter(MergedObjectFormatterType.Simple);
            var sb = simpleFormatter.GetFormattedText(mergedObject, storage);

            // 3. Отображаем результат на форме
            txtResult.Text = sb.ToString();
        }


        ITextObject _createTextObjectFromFile(string path, IUniqueTextLinesStorage storage)
        {
            var fileReader = UtilsFactory.CreateFileReader();
            var sb = fileReader.GetTextFromFile(path);

            using (TextReader reader = new StringReader(sb.ToString()))
            {
                var fileObjectBuilder = ModelFactory.CreateFileObjectBuilder();
                var fileObject = fileObjectBuilder.BuildTextObjectFromReader(reader, path, storage);

                return fileObject;
            }
        }
    }
}
