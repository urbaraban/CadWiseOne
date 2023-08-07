using CadWiseTextFilter;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace CadWiseOne.ViewModels
{
    internal class TextItemVM : NotifyModel
    {

        public event EventHandler<bool> TaskEnded;

        public bool IsEnded
        {
            get => isended_;
            private set
            {
                isended_ = value;
                OnPropertyChanged(nameof(IsEnded));
            }
        }
        private bool isended_ = false;

        public string FileSavePath
        {
            get => TextFile.FileSavePath;
            set
            {
                TextFile.FileSavePath = value;
                OnPropertyChanged(nameof(FileSavePath));
            }
        }

        public string DisplayName => TextFile.Fileinfo.Name;

        public string DisplayFullPath => TextFile.Fullpath;

        public string DisplaySavePath
        {
            get => TextFile.FileSavePath;
            set => TextFile.FileSavePath = value;
        }

        public string DisplayPreview => TextFile.TextPreview;

        private TextFile TextFile { get; }

        public TextItemVM(TextFile file)
        {
            this.TextFile = file;
        }

        public ICommand RemoveCommand => new ActionCommand(() => {
            this.TextFile.Remove();
        });

        public ICommand OpenCommand => new ActionCommand(() => {
            if (File.Exists(FileSavePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(FileSavePath);
                }
                catch
                {
                    MessageBox.Show("Ошибка открытия");
                    IntPtr pidl = ILCreateFromPathW(FileSavePath);
                    SHOpenFolderAndSelectItems(pidl, 0, IntPtr.Zero, 0);
                    ILFree(pidl);
                }
            }
        });

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ILCreateFromPathW(string pszPath);

        [DllImport("shell32.dll")]
        private static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, int cild, IntPtr apidl, int dwFlags);

        [DllImport("shell32.dll")]
        private static extern void ILFree(IntPtr pidl);
    }
}
