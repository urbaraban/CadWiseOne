using CadWiseOne.Properties;
using CadWiseOne.ViewModels;
using CadWiseTextFilter;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace CadWiseOne.Models
{
    internal class TextFileManager : NotifyModel
    {
        public ObservableCollection<TextFile> TextFiles { get; } = new ObservableCollection<TextFile>();

        public int WordLength { get; set; } = 8;
        public bool RemovePunctuation { get; set; } = false;

        public TextFile SelectItem
        {
            get => selecteditem_;
            set
            {
                selecteditem_ = value;
                OnPropertyChanged(nameof(SelectItem));
            }
        }
        private TextFile selecteditem_;

        public TextTask TextTask 
        {
            get => _texttask;
            set
            {
                _texttask = value;
                OnPropertyChanged(nameof(TextTask));
            }
        }
        private TextTask _texttask;

        public ICommand AddFilesCommand => new ActionCommand(() =>
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Settings.Default.LastFolderPath;
            if (openFileDialog.ShowDialog() == true)
            {
                Settings.Default.LastFolderPath = Directory.GetParent(openFileDialog.FileName)?.FullName;
                Settings.Default.Save();

                foreach (string filename in openFileDialog.FileNames)
                {
                    var item = new TextFile(filename);
                    item.Removed += Item_Removed;
                    TextFiles.Add(item);
                }
            }
        });


        public ICommand ConvertFilesCommand => new ActionCommand(async () =>
        {
            if (TextTask == null || TextTask.IsRunning == false)
            {
                foreach (var file in TextFiles)
                {
                    TextTask = new TextTask(file);
                    TextTask.Execut(WordLength, RemovePunctuation);
                }
            }
        });

        private void Item_Removed(object? sender, System.EventArgs e)
        {
            if (sender is TextFile file)
            {
                for (int i = 0; i < TextFiles.Count; i += 1)
                {
                    if (TextFiles[i].Guid == file.Guid)
                    {
                        TextFiles.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
