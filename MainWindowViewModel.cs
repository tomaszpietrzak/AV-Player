using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AV_Player
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            MediaElementObject = new MediaElement();
            MediaElementObject.LoadedBehavior = MediaState.Manual;
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private MediaElement _mediaElementObject;

        public MediaElement MediaElementObject
        {
            get { return _mediaElementObject; }
            set { _mediaElementObject = value;
                NotifyPropertyChanged(); }
        }

        private ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _openFileCommand; }
            set { _openFileCommand = value; }
        }

        public event EventHandler OpenFileEvent;
        private void OpenFile(object obj)
        {
            if (OpenFileEvent != null)
                OpenFileEvent(this, EventArgs.Empty);
        }
    }
}
