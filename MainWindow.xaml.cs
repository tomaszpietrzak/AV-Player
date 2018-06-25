using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AV_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public MainWindow()
        {
            InitializeComponent();
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        
        void OnMouseDownSwitchSize(object sender, RoutedEventArgs args)
        {
            if (WindowState == System.Windows.WindowState.Normal)
            {
                WindowState = System.Windows.WindowState.Maximized;
            }else
            {
                WindowState = System.Windows.WindowState.Normal;
            }
        }
        
        void OnMouseDownCloseWindow(object sender, RoutedEventArgs args)
        {
            Application.Current.Windows[0].Close();
        }
        
        void OnMouseDownMinimizeWindow(object sender, RoutedEventArgs args)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }
        
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        { 
            ProgressStatus.Text = TimeSpan.FromSeconds(TimelineSlider.Value).ToString(@"hh\:mm\:ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((MediaPlayer.Source != null) && (MediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                TimelineSlider.Minimum = 0;
                TimelineSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                TimelineSlider.Value = MediaPlayer.Position.TotalSeconds;
            }
        }

        private void TimelineSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressStatus.Text = TimeSpan.FromSeconds(TimelineSlider.Value).ToString(@"hh\:mm\:ss");
        }

        private void TimelineSliderDragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void TimelineSliderDragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            MediaPlayer.Position = TimeSpan.FromSeconds(TimelineSlider.Value);
        }

        private void TriggerMoveWindow(object sender, MouseButtonEventArgs e)
        {
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            }
        }
    }
}
