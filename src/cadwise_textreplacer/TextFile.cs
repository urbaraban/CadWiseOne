using CadWiseTextFilter.Services;

namespace CadWiseTextFilter
{
    public class TextFile
    {
        public event EventHandler Removed;
        public FileInfo Fileinfo { get; }

        public Guid Guid { get; } = Guid.NewGuid();

        public bool IsLoaded => Fileinfo.Exists == true && string.IsNullOrEmpty(this.TextPreview) == false;
        public string TextPreview { get; }
        public string Fullpath => Fileinfo.FullName;

        public string FileSavePath { get; set; }

        public TextFile(string filepath)
        {
            this.Fileinfo = new FileInfo(filepath);

            if (this.Fileinfo.Exists == true) 
            {
                this.TextPreview = FileReading.GetPrewiewFromFile(this.Fileinfo.FullName);
                this.FileSavePath = this.Fileinfo.Directory + "\\" + Path.GetFileNameWithoutExtension(this.Fileinfo.Name) + "_edited" + this.Fileinfo.Extension;
            }
        }

        public void Remove()
        {
            Removed?.Invoke(this, EventArgs.Empty);
        }
    }
}