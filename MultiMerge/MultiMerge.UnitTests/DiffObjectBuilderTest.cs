using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiMerge.Formatters;
using MultiMerge.Model;
using MultiMerge.UnitTests.Helpers;
using MultiMerge.Utils;

namespace MultiMerge.UnitTests
{

    [TestClass]
    public class DiffObjectBuilderTest
    {
        private static TestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testContext = testContext;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestMethod]
        public void DiffObjectBuilderTest_CreateDiffObject()
        {
            var originalFilePath = DirectoryHelper.GetTestFileFullPath(@"original.txt");
            var version1FilePath = DirectoryHelper.GetTestFileFullPath(@"version1.txt");
            var resultFilePath = DirectoryHelper.GetTestFileFullPath(@"original-version1-result.txt");

            var storage = ModelFactory.CreateUniqueFileLinesStorage();
            var originalFile = DiffObjectTextFromFileHelper.CreateTextObjectFromFile(originalFilePath, storage);
            var version1File = DiffObjectTextFromFileHelper.CreateTextObjectFromFile(version1FilePath, storage);

            var diffObjectBuilder = ModelFactory.CreateDiffObjectBuilder();
            var diffObject = diffObjectBuilder.BuildDiffObjectFromTexts(originalFile, version1File);

            Assert.IsNotNull(diffObject, "Diff объект не создан.");
            Assert.AreEqual(diffObject.OriginalText.Source, originalFilePath, "Diff объект содержит неверное значение источника данных эталонного текста.");
            Assert.AreEqual(diffObject.VersionText.Source, version1FilePath, "Diff объект содержит неверное значение источника данных версионного текста.");
            Assert.IsTrue(diffObject.DiffLines.Count > 0, "Diff объект не содержит данных.");

            // сверяем форматированный результат с эталоном
            var formatter = FormattersFactory.CreateDiffObjectFormatter(DiffObjectFormatterType.Simple);
            var sbNewText = formatter.GetFormattedText(diffObject, storage);

            var reader = UtilsFactory.CreateFileReader();
            var sbEthalonText = reader.GetTextFromFile(resultFilePath);

            var sbNewMd5 = TextMd5Helper.GetMd5FromText(sbNewText);
            var sbEthalonMd5 = TextMd5Helper.GetMd5FromText(sbEthalonText);

            var compareResult = String.Compare(sbNewMd5, sbEthalonMd5, StringComparison.Ordinal);

            Assert.AreEqual(0, compareResult, "Diff объект не соответствует ожидаемому эталонному результату.");

            //var resultFile = string.Format(@"{0}\test-result.txt", DirectoryHelper.GetCurrentExeDirectory());
            // ObjectToFileHelper.WriteDiffObjectToFile(diffObject, storage, resultFile, true);
        }

        
    }
}
