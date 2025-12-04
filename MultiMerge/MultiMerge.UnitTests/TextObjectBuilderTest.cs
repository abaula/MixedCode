using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiMerge.Model;
using MultiMerge.UnitTests.Helpers;

namespace MultiMerge.UnitTests
{
    [TestClass]
    public class TextObjectBuilderTest
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
        public void FileObjectBuilderTest_CreateText()
        {
            var path = DirectoryHelper.GetTestFileFullPath(@"original.txt");

            using(var reader = new StreamReader(path))
            {
                var storage = ModelFactory.CreateUniqueFileLinesStorage();
                var textObjectBuilder = ModelFactory.CreateFileObjectBuilder();
                var textObject = textObjectBuilder.BuildTextObjectFromReader(reader, path, storage);

                Assert.IsNotNull(textObject, "Файловый объект не создан.");
                Assert.AreEqual(path, textObject.Source, "Неверный путь файла в файловом объекте.");
                Assert.AreEqual(24, textObject.Lines.Count, "Неверное количество строк в файловом объекте.");

            }
        }
    }
}
