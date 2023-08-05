using CadWiseTextReplacer.Services;

namespace CadWiseTextReplacer
{
    public class TextFileItem
    {
        public bool IsEmpty => FileInfo.Exists == false || string.IsNullOrEmpty(this.TextPreview);

        public string TextPreview { get; }

        public FileInfo FileInfo { get; }

        public TextFileItem(string filepath)
        {
            this.FileInfo = new FileInfo(filepath);

            if (this.FileInfo.Exists == true) 
            {
                this.TextPreview = FileReading.GetPrewiewFromFile(this.FileInfo.FullName);
            }
        }

       
    }
}