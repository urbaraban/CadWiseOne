using CadWiseTextReplacer;
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
        public bool IsReady => TextTask.Status;

        public string FileSavePath
        {
            get => TextTask.FileSavePath;
            set
            {
                TextTask.FileSavePath = value;
                OnPropertyChanged(nameof(FileSavePath));
            }
        }

        public TextFileTask TextTask { get; }
        public TextItemVM(TextFileTask item)
        {
            this.TextTask = item;
            this.TextTask.TaskEnded += TextTask_TaskEnded;
        }

        private void TextTask_TaskEnded(object? sender, bool e)
        {
            OnPropertyChanged(nameof(IsReady));
        }

        public ICommand RemoveCommand => new ActionCommand(() => {
            this.TextTask.Remove();
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
