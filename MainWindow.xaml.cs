using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        private bool userIsDraggingSlider = false;
        public TimeSpan TimeoutToHide { get; private set; }
        public DateTime LastMouseMove { get; private set; }
        public bool IsUIHidden { get; private set; }
        public bool IsVolumeSliderHidden { get; private set; }

        public MainWindow()
        {
            //InitializeComponent();
            
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += Timer_Tick;
            //timer.Start();

            //(this.DataContext as MainWindowViewModel).PlayMediaEvent += OnPlayMedia;
        }

        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;

            DispatcherTimer timer1s = new DispatcherTimer();
            timer1s.Interval = TimeSpan.FromSeconds(1);
            timer1s.Tick += TimelineTick;
            timer1s.Tick += UITick;
            timer1s.Start();

            TimeoutToHide = TimeSpan.FromSeconds(5);

            (this.DataContext as MainWindowViewModel).PlayMediaEvent += OnPlayMedia;
            (this.DataContext as MainWindowViewModel).PauseMediaEvent += OnPauseMedia;
            (this.DataContext as MainWindowViewModel).RewindMediaEvent += OnRewindMedia;
            (this.DataContext as MainWindowViewModel).ForwardMediaEvent += OnForwardMedia;
            (this.DataContext as MainWindowViewModel).LoadedMediaEvent += OnLoadedMedia;
        }

        void OnPlayMedia(object sender, EventArgs args)
        {
            MediaPlayer.Play();
        }

        void OnPauseMedia(object sender, EventArgs args)
        {
            MediaPlayer.Pause();
        }

        void OnRewindMedia(object sender, EventArgs args)
        {
            MediaPlayer.Position = TimeSpan.FromSeconds(MediaPlayer.Position.TotalSeconds - 10);
        }

        void OnForwardMedia(object sender, EventArgs args)
        {
            MediaPlayer.Position = TimeSpan.FromSeconds(MediaPlayer.Position.TotalSeconds + 10);
        }

        void OnLoadedMedia(object sender, EventArgs args)
        {

        }
        
        void OnSwitchSize(object sender, RoutedEventArgs args)
        {
            if (WindowState == System.Windows.WindowState.Normal)
            {
                WindowState = System.Windows.WindowState.Maximized;
            }else
            {
                WindowState = System.Windows.WindowState.Normal;
            }
        }
        
        void OnCloseWindow(object sender, RoutedEventArgs args)
        {
            Application.Current.Windows[0].Close();
        }
        
        void OnMinimizeWindow(object sender, RoutedEventArgs args)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }
        
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        { 
            ProgressStatus.Text = TimeSpan.FromSeconds(TimelineSlider.Value).ToString(@"hh\:mm\:ss");
        }

        private void TimelineTick(object sender, EventArgs e)
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

        void HideUI(object sender, MouseEventArgs e)
        {
            LastMouseMove = DateTime.Now;

            if (IsUIHidden)
            {
                Cursor = Cursors.Arrow;
                Overlay.Visibility = Visibility.Visible;
                IsUIHidden = false;
            }
        }

        private void UITick(object sender, EventArgs e)
        {
            TimeSpan elaped = DateTime.Now - LastMouseMove;
            if (elaped >= TimeoutToHide && !IsUIHidden)
            {
                Cursor=Cursors.None;
                Overlay.Visibility = Visibility.Hidden;
                IsUIHidden = true;
            }
        }

        void OnSwitchVolumSliderVisibility(object sender, RoutedEventArgs args)
        {
            if (VolumeSlider.Visibility == Visibility.Visible)
            {
                VolumeSlider.Visibility = Visibility.Hidden;
            }else
            {
                VolumeSlider.Visibility = Visibility.Visible;
            }
        }
        
    }
}

