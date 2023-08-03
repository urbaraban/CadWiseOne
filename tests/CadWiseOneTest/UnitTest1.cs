using CadWiseTextReplacer;

namespace CadWiseOneTest
{
    public class Tests
    {
        private string[] files = new string[]
        {
            "C:\\Users\\Urbaraban\\source\\repos\\CadWiseOne\\tests\\CadWiseOneTest\\testfiles\\pim_installmgr.log",
            "C:\\Users\\Urbaraban\\source\\repos\\CadWiseOne\\tests\\CadWiseOneTest\\testfiles\\Пелевин. Transhumanism Inc.fb2"
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextFileItemLoadTest()
        {
            var item = new TextFileItem(files[0]);
            Assert.Pass();
        }

        [Test]
        public void FilePrewiewReadingTest()
        {
            var item = new TextFileItem(files[0]);
            Assert.IsNotNull(item.TextPreview);
            Assert.IsNotEmpty(item.TextPreview);
        }

        [Test]
        public void TextTransformTest()
        {
            var item = new TextFileItem(files[0]);
            item.TextTransform(-1, false);
            Assert.IsTrue(File.Exists(item.SaveAsPath));
        }
    }
}