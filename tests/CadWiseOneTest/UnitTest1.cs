using CadWiseTextReplacer;
using CadWiseTextReplacer.Services;
using NUnit.Framework.Internal;

namespace CadWiseOneTest
{
    public class Tests
    {
        private string[] files = new string[]
        {
            "testfiles\\polish_notatoin_lib.md",
            "testfiles\\RESULT_VALGRIND.txt",
            "testfiles\\�������. Transhumanism Inc.fb2"
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextFileItemLoadTest()
        {
            var item = new TextFileTask(files[0]);
            Assert.IsTrue(item.IsLoaded);
        }

        [Test]
        public void FilePrewiewReadingTest()
        {
            var item = new TextFileTask(files[0]);
            Assert.IsNotNull(item.TextPreview);
            Assert.IsNotEmpty(item.TextPreview);
        }

        [Test]
        public void TextTransformRemovePunctTest()
        {
            var item = new TextFileTask(files[0]);
            item.Execut(-1, true);
            Assert.IsTrue(File.Exists(item.FileSavePath));
            //File.Delete(item.FileInfo.FullName + "_edited");
        }

        [Test]
        public void TextTransformWordLengthText()
        {
            var item = new TextFileTask(files[0]);
            item.Execut(5, false);

            Assert.IsTrue(File.Exists(item.FileSavePath));
        }

        [Test]
        public void TextTransformWordRemoveAll()
        {
            var item = new TextFileTask(files[0]);
            item.Execut(0, true);
            Assert.IsTrue(File.Exists(item.FileSavePath));
        }
    }
}