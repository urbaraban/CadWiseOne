using CadWiseOne.Properties;
using CadWiseTextReplacer;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadWiseOne.ViewModels
{
    internal class TextEditorVM : NotifyModel
    {
        public ObservableCollection<TextFileTask> TextTasks { get; } = new ObservableCollection<TextFileTask>();
        public int WordLength { get; set; } = 8;
        public bool RemovePunctuation { get; set; } = false;

        public TextFileTask SelectItem 
        {
            get => selecteditem_;
            set
            {
                selecteditem_ = value;
                OnPropertyChanged(nameof(SelectItem));
            }
        }
        private TextFileTask selecteditem_;


        private Task Process { get; set; }

        public ICommand AddFilesCommand => new ActionCommand(() => {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Settings.Default.LastFolderPath;
            if (openFileDialog.ShowDialog() == true)
            {
                Settings.Default.LastFolderPath = Directory.GetParent(openFileDialog.FileName)?.FullName;
                Settings.Default.Save();

                foreach (string filename in openFileDialog.FileNames)
                {
                    var item = new TextFileTask(filename);
                    item.Removed += Item_Removed;
                    TextTasks.Add(item);
                }     
            }
        });

        private void Item_Removed(object? sender, System.EventArgs e)
        {
            if (sender is TextFileTask task)
            {
                this.TextTasks.Remove(task);
            }
        }

        public ICommand ConvertFilesCommand => new ActionCommand(async () => {
            if (Process == null || Process.Status > TaskStatus.Running) {
                Process = Task.Run(() =>
                {
                    foreach (TextFileTask task in TextTasks)
                    {
                        task.Execut(WordLength, RemovePunctuation);
                    }
                });
            }
        });
    }
}
