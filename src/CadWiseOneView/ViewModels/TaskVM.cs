using CadWiseTextFilter;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Windows.Input;

namespace CadWiseOne.ViewModels
{
    internal class TaskVM : NotifyModel, IDisposable
    {
        public int LinesPosition
        {
            get => _linesPosition;
            set
            {
                _linesPosition = value;
                OnPropertyChanged(nameof(LinesPosition));
            }
        }
        private int _linesPosition;

        public int LinesCount 
        {
            get => _linesCount;
            set
            {
                _linesCount = value;
                OnPropertyChanged(nameof(LinesCount));
            }
        }
        private int _linesCount;

        public string DisplayName => TextTask.TextFile.Fileinfo.Name;

        private TextTask TextTask { get; }
        public TaskVM(TextTask textTask)
        {
            TextTask = textTask;
            TextTask.PostProgress += PostProgress;
        }

        public ICommand CancelCommand => new ActionCommand(() =>
        {
            TextTask.Cancel();
        });

        public void Dispose()
        {
            if (this.TextTask.PostProgress != null)
            {
                this.TextTask.PostProgress -= PostProgress;
            }

        }

        private void PostProgress(int linesposition, int linescount)
        {
            this.LinesPosition = linesposition;
            this.LinesCount = linescount;
        }

    }
}
