using CadWiseTextFilter.Services;

namespace CadWiseTextFilter
{
    public class TextTask
    {
        public ProgressUpdate PostProgress { get; set; }
        public delegate void ProgressUpdate(int index, int max);

        public bool IsRunning => task != null && task.Status < TaskStatus.RanToCompletion;
        public int LinesPosition { get; private set; }
        public int LinesCount { get; }

        public TextFile TextFile { get; }

        private CancellationTokenSource cancellationTokenSource { get; } = new CancellationTokenSource();
        private Task task { get; set; }

        public TextTask(TextFile textFile) 
        {
            this.TextFile = textFile;

            if (textFile.IsLoaded == true)
            {
                LinesCount = File.ReadAllLines(this.TextFile.Fileinfo.FullName).Length;
            }
        }

        public TextTask(string filepath) : this(new TextFile(filepath)) { }

        public void Execut(int word_length, bool remove_punctuation)
        {
            if (TextFile.Fileinfo.Exists == true)
            {
                CancellationToken cancellation = this.cancellationTokenSource.Token;

                this.task = Task.Run(() =>
                {
                    FileStream fileStream = new FileStream(TextFile.Fullpath, FileMode.Open, FileAccess.Read);
                    using (StreamReader file = new StreamReader(fileStream))
                    {
                        StreamWriter writer = new StreamWriter(TextFile.FileSavePath, false);
                        int counter = 0;
                        string ln;

                        while ((ln = file.ReadLine()) != null && cancellation.IsCancellationRequested == false)
                        {
                            if (ln.Length > 0)
                            {
                                string edited = TextEditing.EditText(ln, word_length, remove_punctuation);
                                writer.Write(edited);
                            }
                            counter += 1;
                            PostProgress?.Invoke(counter, LinesCount);
                        }
                        writer.Close();
                        file.Close();
                    }
                }, cancellation);
            }
        }

        public void Cancel()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
