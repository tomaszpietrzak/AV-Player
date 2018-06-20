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
            OpenSettingsWindowCommand = new RelayCommand(OpenSettingsWindow);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand _openSettingsWindowCommand;
        public ICommand OpenSettingsWindowCommand
        {
            get { return _openSettingsWindowCommand; }
            set { _openSettingsWindowCommand = value; }
        }

        private void OpenSettingsWindow(object obj)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Show();
        }
    }
}
