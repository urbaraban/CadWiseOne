using CadWiseTextReplacer.Services;

namespace CadWiseTextReplacer
{
    public class TextFileTask
    { 
        public bool IsLoaded => Fileinfo.Exists == true && string.IsNullOrEmpty(this.TextPreview);

        public int LinesCount { get; }

        public string TextPreview { get; }

        public FileInfo Fileinfo { get; }

        public string FileSavePath { get; set; }

        public TextFileTask(string filepath)
        {
            this.Fileinfo = new FileInfo(filepath);

            if (this.Fileinfo.Exists == true) 
            {
                this.TextPreview = FileReading.GetPrewiewFromFile(this.Fileinfo.FullName);
                LinesCount = File.ReadAllLines(this.Fileinfo.FullName).Length;
                this.FileSavePath = this.Fileinfo.Directory + "\\" + Path.GetFileNameWithoutExtension(this.Fileinfo.Name) + "_edited" + this.Fileinfo.Extension;
            }
        }
    }
}