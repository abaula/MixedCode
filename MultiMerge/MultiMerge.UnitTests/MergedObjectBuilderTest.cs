using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiMerge.Formatters;
using MultiMerge.Model;
using MultiMerge.UnitTests.Helpers;
using MultiMerge.Utils;

namespace MultiMerge.UnitTests
{
    [TestClass]
    public class MergedObjectBuilderTest
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
        public void MergedObjectBuilderTest_Merge3Files()
        {
            var originalFilePath = DirectoryHelper.GetTestFileFullPath(@"original.txt");
            var version1FilePath = DirectoryHelper.GetTestFileFullPath(@"version1.txt");
            var version2FilePath = DirectoryHelper.GetTestFileFullPath(@"version2.txt");
            var resultFilePath = DirectoryHelper.GetTestFileFullPath(@"merge3files-result.txt");

            var storage = ModelFactory.CreateUniqueFileLinesStorage();
            var originalFile = DiffObjectTextFromFileHelper.CreateTextObjectFromFile(originalFilePath, storage);
            var version1File = DiffObjectTextFromFileHelper.CreateTextObjectFromFile(version1FilePath, storage);
            var version2File = DiffObjectTextFromFileHelper.CreateTextObjectFromFile(version2FilePath, storage);


            var diffObjectBuilder = ModelFactory.CreateDiffObjectBuilder();
            var diffObjectsList = new List<IDiffObject>();
            
            var diffObject1 = diffObjectBuilder.BuildDiffObjectFromTexts(originalFile, version1File);
            diffObjectsList.Add(diffObject1);

            var diffObject2 = diffObjectBuilder.BuildDiffObjectFromTexts(originalFile, version2File);
            diffObjectsList.Add(diffObject2);

            var mergedObjectBuilder = ModelFactory.CreateMergedObjectBuilder();
            var mergedObject = mergedObjectBuilder.BuildMergedObjectFromDiffs(diffObjectsList);

            Assert.IsNotNull(mergedObject, "Объект IMergedObject не создан.");
            Assert.IsTrue(mergedObject.Blocks.Any(), "Объект IMergedObject пуст - не содержит строк.");

            // сверяем форматированный результат с эталоном
            var formatter = FormattersFactory.CreateMergedObjectFormatter(MergedObjectFormatterType.Simple);
            var sbNewText = formatter.GetFormattedText(mergedObject, storage);

            var reader = UtilsFactory.CreateFileReader();
            var sbEthalonText = reader.GetTextFromFile(resultFilePath);

            var sbNewMd5 = TextMd5Helper.GetMd5FromText(sbNewText);
            var sbEthalonMd5 = TextMd5Helper.GetMd5FromText(sbEthalonText);

            var compareResult = String.Compare(sbNewMd5, sbEthalonMd5, StringComparison.Ordinal);

            Assert.AreEqual(0, compareResult, "Merged объект не соответствует ожидаемому эталонному результату.");

            //var resultFile = string.Format(@"{0}\test-result.txt", DirectoryHelper.GetCurrentExeDirectory());
            //ObjectToFileHelper.WriteMergedObjectToFile(mergedObject, storage, resultFile, true);
        }
    }
}
