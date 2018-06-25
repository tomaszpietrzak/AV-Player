using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AV_Player
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            SettingsViewModel settingsViewModel = new SettingsViewModel();
            this.DataContext = settingsViewModel;

            cmbStyle.ItemsSource =
                from dir in System.IO.Directory.GetDirectories("Themes")
                where System.IO.File.Exists(System.IO.Path.Combine(dir, "Main.Xaml"))
                select new ThemeData()
                {
                    ThemeName = System.IO.Path.GetFileName(dir),
                    ThemeFile = System.IO.Path.Combine(dir, "Main.Xaml")
                };
        }

        private void cmbStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStyle.SelectedIndex == -1) return;
            using (FileStream fs = new FileStream(((ThemeData)cmbStyle.SelectedValue).ThemeFile.ToString(), FileMode.Open))
            {
                ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(fs);
                Resources.MergedDictionaries.Clear();
                Resources.MergedDictionaries.Add(dic);
            }
        }

    }

    internal class ThemeData
    {
        public ThemeData()
        {
        }

        public object ThemeName { get; set; }
        public object ThemeFile { get; set; }
    }
}
