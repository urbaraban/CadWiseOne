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
            "testfiles\\Пелевин. Transhumanism Inc.fb2"
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextFileItemLoadTest()
        {
            var item = new TextFileItem(files[0]);
            Assert.IsFalse(item.IsEmpty);
        }

        [Test]
        public void FilePrewiewReadingTest()
        {
            var item = new TextFileItem(files[0]);
            Assert.IsNotNull(item.TextPreview);
            Assert.IsNotEmpty(item.TextPreview);
        }

        [Test]
        public void TextTransformRemovePunctTest()
        {
            var item = new TextFileItem(files[0]);
            FileWriting.TextTransform(
                item.FileInfo.FullName, 
                item.FileInfo.FullName + "_edited",
                - 1, true);

            Assert.IsTrue(File.Exists(item.FileInfo.FullName + "_edited"));
            //File.Delete(item.FileInfo.FullName + "_edited");
        }

        [Test]
        public void TextTransformWordLengthText()
        {
            var item = new TextFileItem(files[0]);
            FileWriting.TextTransform(
                item.FileInfo.FullName,
                item.FileInfo.FullName + "_edited",
                5, false);

            Assert.IsTrue(File.Exists(item.FileInfo.FullName + "_edited"));
        }

        [Test]
        public void TextTransformWordRemoveAll()
        {
            var item = new TextFileItem(files[0]);
            FileWriting.TextTransform(
                item.FileInfo.FullName,
                item.FileInfo.FullName + "_edited",
                0, true);

            Assert.IsTrue(File.Exists(item.FileInfo.FullName + "_edited"));
        }
    }
}