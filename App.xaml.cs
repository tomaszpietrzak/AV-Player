using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AV_Player
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            // Application is running
            // Process command line args

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            if (e.Args.Length==1)
            {
                mainWindowViewModel.MediaSource = new Uri(e.Args[0]);
            }

            MainWindow mainWindow = new MainWindow(mainWindowViewModel);
            mainWindow.Show();
        }
    }
}
