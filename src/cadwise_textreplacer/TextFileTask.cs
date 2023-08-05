using CadWiseTextReplacer.Services;

namespace CadWiseTextReplacer
{
    public class TextFileTask
    {
        public event EventHandler Removed;

        public bool IsLoaded => Fileinfo.Exists == true && string.IsNullOrEmpty(this.TextPreview);
        public bool Status { get; private set; } = false;

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

        public void Execut(int word_length, bool remove_punctuation)
        {
            FileWriting.TextTransform(
                            this.Fileinfo.FullName,
                            this.FileSavePath,
                            word_length,
                            remove_punctuation);
        }

        public void Remove()
        {
            this.Removed?.Invoke(this, EventArgs.Empty);
        }
    }
}