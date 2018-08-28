using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AV_Player
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        

        public SettingsViewModel()
        {
            var s=Properties.Settings.Default.Properties;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string LeftKey
        {
            get
            {
                return Properties.Settings.Default.LeftKey;
            }
            set
            {
                Properties.Settings.Default.LeftKey = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
