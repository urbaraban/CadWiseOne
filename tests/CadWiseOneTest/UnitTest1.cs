using CadWiseTextReplacer;

namespace CadWiseOneTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FilePrewiewReadingTest()
        {
            Assert.Pass();
        }

        [Test]
        public void TextFileItemLoadTest()
        {
            var item = new TextFileItem("C:\\Users\\Urbaraban\\Documents\\pim_installmgr.log");
            Assert.IsNotEmpty(item.TextPreview);
        }
    }
}